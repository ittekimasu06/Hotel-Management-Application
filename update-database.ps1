Write-Host "Running EF6 Update-Database"
& "$(Get-ItemProperty 'HKLM:\SOFTWARE\Microsoft\MSBuild\ToolsVersions\*').MSBuildToolsPath\MSBuild.exe" `
    .\QuanLyKhachSan.csproj `
    /t:update-database `
    /p:Configuration=Release `
    /p:ConnectionStrings__DefaultConnection="Data source=(localDB)\MSSQlLocalDB;AttachDbFilename=|DataDirectory|\QuanLyKhachSan.mdf;Initial Catalog=DefaultConnection;Integrated Security=True"  `
    /verbosity:normal
