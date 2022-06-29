# Only install when sshd service is not available
if (-Not (Get-Service sshd -ErrorAction SilentlyContinue))
{
    # Install choco
    Set-ExecutionPolicy Bypass -Scope Process -Force; [System.Net.ServicePointManager]::SecurityProtocol = [System.Net.ServicePointManager]::SecurityProtocol -bor 3072; iex ((New-Object System.Net.WebClient).DownloadString('https://chocolatey.org/install.ps1'))

    # Install SSH
    choco install openssh --yes

    # Install SSH server
    &"C:\Program Files\OpenSSH-Win64\install-sshd.ps1"

    # Start SSH service
    Set-Service sshd -StartupType Automatic
    Start-Service sshd

    # Enable password authentication
    # For local usage ONLY we enable empty passwords
    $FilePath = "$env:PROGRAMDATA\ssh\sshd_config"
    (Get-Content $FilePath).Replace('#PasswordAuthentication yes','PasswordAuthentication yes').Replace('#PermitEmptyPasswords no', 'PermitEmptyPasswords yes') | Set-Content $FilePath

    Restart-Service sshd

    # Add user
    net user debug /add
    net localgroup administrators debug /add
}

Start-Service sshd