Add-Type -AssemblyName System.Windows.Forms
Add-Type -AssemblyName System.Drawing

# --- FORM ---
$form = New-Object System.Windows.Forms.Form
$form.Text = "Ultra Fast File Copy GUI"
$form.Size = New-Object System.Drawing.Size(500,400)
$form.StartPosition = "CenterScreen"
$form.Topmost = $true

# LABELS
$label1 = New-Object System.Windows.Forms.Label
$label1.Text = "Source Folder 1:"
$label1.Location = "10,20"
$label1.AutoSize = $true
$form.Controls.Add($label1)

$label2 = New-Object System.Windows.Forms.Label
$label2.Text = "Source Folder 2:"
$label2.Location = "10,80"
$label2.AutoSize = $true
$form.Controls.Add($label2)

$label3 = New-Object System.Windows.Forms.Label
$label3.Text = "Destination Folder:"
$label3.Location = "10,140"
$label3.AutoSize = $true
$form.Controls.Add($label3)

# TEXTBOXES
$tb1 = New-Object System.Windows.Forms.TextBox
$tb1.Location = "10,45"
$tb1.Width = 350
$form.Controls.Add($tb1)

$tb2 = New-Object System.Windows.Forms.TextBox
$tb2.Location = "10,105"
$tb2.Width = 350
$form.Controls.Add($tb2)

$tb3 = New-Object System.Windows.Forms.TextBox
$tb3.Location = "10,165"
$tb3.Width = 350
$form.Controls.Add($tb3)

# --- BROWSE BUTTONS ---
$browse1 = New-Object System.Windows.Forms.Button
$browse1.Text = "Browse"
$browse1.Location = "370,43"
$browse1.Width = 80
$browse1.Add_Click({
    $dialog = New-Object System.Windows.Forms.FolderBrowserDialog
    if ($dialog.ShowDialog() -eq "OK") {
        $tb1.Text = $dialog.SelectedPath
    }
})
$form.Controls.Add($browse1)

$browse2 = New-Object System.Windows.Forms.Button
$browse2.Text = "Browse"
$browse2.Location = "370,103"
$browse2.Width = 80
$browse2.Add_Click({
    $dialog = New-Object System.Windows.Forms.FolderBrowserDialog
    if ($dialog.ShowDialog() -eq "OK") {
        $tb2.Text = $dialog.SelectedPath
    }
})
$form.Controls.Add($browse2)

$browse3 = New-Object System.Windows.Forms.Button
$browse3.Text = "Browse"
$browse3.Location = "370,163"
$browse3.Width = 80
$browse3.Add_Click({
    $dialog = New-Object System.Windows.Forms.FolderBrowserDialog
    if ($dialog.ShowDialog() -eq "OK") {
        $tb3.Text = $dialog.SelectedPath
    }
})
$form.Controls.Add($browse3)

# STATUS BOX
$status = New-Object System.Windows.Forms.TextBox
$status.Multiline = $true
$status.ScrollBars = "Vertical"
$status.Location = "10,210"
$status.Size = "450,100"
$form.Controls.Add($status)

# --- START BUTTON ---
$startBtn = New-Object System.Windows.Forms.Button
$startBtn.Text = "START COPY"
$startBtn.Location = "180,320"
$startBtn.Width = 120
$startBtn.Height = 30

$startBtn.Add_Click({
    $src1 = $tb1.Text
    $src2 = $tb2.Text
    $dest = $tb3.Text

    if (-not (Test-Path $src1) -or -not (Test-Path $src2) -or -not (Test-Path $dest)) {
        [System.Windows.Forms.MessageBox]::Show("Please select valid folders!", "Error")
        return
    }

    $status.AppendText("Starting ultra-fast copy...`r`n")

    $sources = @($src1, $src2)

    foreach ($src in $sources) {
        $status.AppendText("Copying from $src ...`r`n")

        $cmd = "robocopy `"$src`" `"$dest`" /E /MT:64 /R:1 /W:1 /Z /ZB /J /COPYALL /DCOPY:DAT /V"
        $output = cmd.exe /c $cmd

        $status.AppendText($output + "`r`n")
        $status.AppendText("Done copying from $src `r`n`r`n")
    }

    $status.AppendText("ALL COPY OPERATIONS COMPLETED")
})

$form.Controls.Add($startBtn)

# SHOW FORM
$form.ShowDialog()