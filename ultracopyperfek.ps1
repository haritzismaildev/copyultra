Add-Type -AssemblyName System.Windows.Forms
Add-Type -AssemblyName System.Drawing

# --- FORM ---
$form = New-Object System.Windows.Forms.Form
$form.Text = "Ultra Fast File Copy GUI with Progress"
$form.Size = New-Object System.Drawing.Size(550,450)
$form.StartPosition = "CenterScreen"
$form.Topmost = $true

# LABELS
$labels = @{
    l1 = "Source Folder 1:"
    l2 = "Source Folder 2:"
    l3 = "Destination Folder:"
}

$y = 20
foreach ($key in $labels.Keys) {
    $label = New-Object System.Windows.Forms.Label
    $label.Text = $labels[$key]
    $label.Location = "10,$y"
    $label.AutoSize = $true
    $form.Controls.Add($label)
    $y += 60
}

# TEXTBOXES
$tb1 = New-Object System.Windows.Forms.TextBox; $tb1.Location="10,45"; $tb1.Width=380
$tb2 = New-Object System.Windows.Forms.TextBox; $tb2.Location="10,105"; $tb2.Width=380
$tb3 = New-Object System.Windows.Forms.TextBox; $tb3.Location="10,165"; $tb3.Width=380

$form.Controls.Add($tb1)
$form.Controls.Add($tb2)
$form.Controls.Add($tb3)

# --- BROWSE BUTTONS ---
function AddBrowseButton($x, $y, $textbox) {
    $btn = New-Object System.Windows.Forms.Button
    $btn.Text = "Browse"
    $btn.Location = "$x,$y"
    $btn.Width = 80
    $btn.Add_Click({
        $dialog = New-Object System.Windows.Forms.FolderBrowserDialog
        if ($dialog.ShowDialog() -eq "OK") {
            $textbox.Text = $dialog.SelectedPath
        }
    })
    $form.Controls.Add($btn)
}

AddBrowseButton 400 43 $tb1
AddBrowseButton 400 103 $tb2
AddBrowseButton 400 163 $tb3

# --- PROGRESS BAR ---
$progress = New-Object System.Windows.Forms.ProgressBar
$progress.Location = "10,230"
$progress.Size = "500,30"
$progress.Minimum = 0
$progress.Maximum = 100
$form.Controls.Add($progress)

# PROGRESS LABEL
$progressLabel = New-Object System.Windows.Forms.Label
$progressLabel.Location = "10,265"
$progressLabel.Size = "500,20"
$progressLabel.Text = "Progress: 0%"
$form.Controls.Add($progressLabel)

# STATUS BOX
$status = New-Object System.Windows.Forms.TextBox
$status.Multiline = $true
$status.ScrollBars = "Vertical"
$status.Location = "10,300"
$status.Size = "500,80"
$form.Controls.Add($status)

# --- START BUTTON ---
$startBtn = New-Object System.Windows.Forms.Button
$startBtn.Text = "START COPY"
$startBtn.Location = "210,390"
$startBtn.Width = 120
$form.Controls.Add($startBtn)


# ========== FUNCTION: CALCULATE TOTAL SIZE & FILE COUNT ==========
function Get-FolderStats($folders) {
    $totalBytes = 0
    $totalFiles = 0

    foreach ($folder in $folders) {
        if (Test-Path $folder) {
            $files = Get-ChildItem -Path $folder -Recurse -File -ErrorAction SilentlyContinue
            foreach ($file in $files) {
                $totalBytes += $file.Length
                $totalFiles++
            }
        }
    }

    return [PSCustomObject]@{
        Bytes = $totalBytes
        Files = $totalFiles
    }
}

# ========== COPY PROCESS ==========
$startBtn.Add_Click({

    $src1 = $tb1.Text
    $src2 = $tb2.Text
    $dest = $tb3.Text

    if (-not (Test-Path $src1) -or -not (Test-Path $src2) -or -not (Test-Path $dest)) {
        [System.Windows.Forms.MessageBox]::Show("Please select valid folders!")
        return
    }

    $sources = @($src1, $src2)

    # --- Hitung total size & total file untuk progress real ---
    $status.AppendText("Calculating total files & size...`r`n")
    $stats = Get-FolderStats $sources
    $totalBytes = [double]$stats.Bytes

    if ($totalBytes -eq 0) {
        [System.Windows.Forms.MessageBox]::Show("Source folders are empty!")
        return
    }

    $status.AppendText("Total Size: $([Math]::Round($totalBytes/1GB,2)) GB`r`n")
    $status.AppendText("Total Files: $($stats.Files)`r`n`r`n")

    $bytesCopied = 0

    foreach ($src in $sources) {
        $status.AppendText("Copying from $src ...`r`n")

        $files = Get-ChildItem -Path $src -Recurse -File

        foreach ($file in $files) {
            $relative = $file.FullName.Substring($src.Length).TrimStart("\")
            $targetFile = Join-Path $dest $relative
            $targetDir = Split-Path $targetFile

            if (!(Test-Path $targetDir)) { New-Item -ItemType Directory -Path $targetDir | Out-Null }

            Copy-Item -Path $file.FullName -Destination $targetFile -Force

            # update progress
            $bytesCopied += $file.Length
            $percent = [Math]::Round(($bytesCopied / $totalBytes) * 100)

            if ($percent -gt 100) { $percent = 100 }

            $progress.Value = $percent
            $progressLabel.Text = "Progress: $percent%"

            $status.AppendText("Copied: $relative`r`n")

            $form.Refresh()
        }

        $status.AppendText("Completed folder: $src`r`n`r`n")
    }

    $progress.Value = 100
    $progressLabel.Text = "Progress: 100% (Completed)"
    $status.AppendText("ALL COPY OPERATIONS COMPLETED")
})

$form.ShowDialog()