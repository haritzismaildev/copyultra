# Tentukan dua folder sumber
$Source1 = "D:\dari_hdd_ex_ps3\musik"   # Ganti sesuai sumber pertama
$Source2 = "D:\dari_hdd_ex_ps3\nitip_pideo"   # Ganti sesuai sumber kedua

# Folder tujuan = Downloads\mediadull
$Destination = "$env:USERPROFILE\Downloads\mediadull"

# Array berisi semua sumber
$Sources = @($Source1, $Source2)

# Loop untuk mengeksekusi copy dengan ultra speed
foreach ($src in $Sources) {
    robocopy $src $Destination /E /MT:64 /R:1 /W:1 /Z /ZB /J /COPYALL /DCOPY:DAT /V /ETA
}