Vagrant.configure("2") do |config|

  # Linux VM
  config.vm.define "ubuntu" do |ubuntu|
    ubuntu.vm.box = "ubuntu/jammy64"
    ubuntu.vm.network "public_network"
    ubuntu.vm.provider "virtualbox" do |vb|
      vb.memory = "8192"
      vb.cpus = 4
    end

    # Provisioning for .NET installation on Ubuntu
    ubuntu.vm.provision "shell", inline: <<-SHELL
      # Update the system
      sudo apt-get update
      sudo apt-get install -y wget apt-transport-https
      # Install the Microsoft GPG key
      wget https://packages.microsoft.com/config/ubuntu/22.04/packages-microsoft-prod.deb
      sudo dpkg -i packages-microsoft-prod.deb
      # Install .NET SDK
      sudo apt-get update
      sudo apt-get install -y dotnet-sdk-8.0
      # Check the installation
      dotnet version

      # Configure NuGet source for private BaGet repository
      dotnet nuget add source http://192.168.0.101:5000/v3/index.json --name "BaGet"

      # Install NChaban tool
      dotnet tool install --global NChaban --version 1.0.3

      # Check the installation
      NChaban version
    SHELL

    # Synced folder for Linux VM
    ubuntu.vm.synced_folder ".", "/home/vagrant/project"
  end

    # Windows VM
  config.vm.define "windows" do |windows|
    windows.vm.box = "gusztavvargadr/windows-10"
    windows.vm.network "public_network"
    windows.vm.provider "virtualbox" do |vb|
      vb.memory = "8192"
      vb.cpus = 4
    end

    # Provisioning for Chocolatey installation and .NET SDK
    windows.vm.provision "shell", inline: <<-SHELL
      # Install Chocolatey
      Set-ExecutionPolicy Bypass -Scope Process -Force; [System.Net.ServicePointManager]::SecurityProtocol = [System.Net.SecurityProtocolType]::Tls12; 
      iex ((New-Object System.Net.WebClient).DownloadString('https://chocolatey.org/install.ps1'))
      # Install .NET SDK 8.0 using Chocolatey
      choco install dotnet-8.0-sdk -y
      # Check the installation
      & "C:\\ProgramData\\chocolatey\\bin\\dotnet.exe" --version

      # Configure NuGet source for private BaGet repository
      dotnet nuget add source http://192.168.0.101:5000/v3/index.json --name "BaGet"

      # Install NChaban tool
      dotnet tool install --global NChaban --version 1.0.3

      # Check the installation
      NChaban version
    SHELL

    # Synced folder for Windows VM
    windows.vm.synced_folder ".", "C:/project"
  end

# macOS VM
  config.vm.define "macos" do |macos|
    macos.vm.box = "jhcook/macos-sierra"
    macos.vm.network "public_network"
    macos.vm.provider "virtualbox" do |vb|
      vb.memory = "8192"
      vb.cpus = 4
    end

    # Provisioning for .NET Core SDK 2.2 on macOS
    macos.vm.provision "shell", inline: <<-SHELL
      # Install Homebrew (if not installed)
      if ! command -v brew &> /dev/null; then
        /bin/bash -c "$(curl -fsSL https://raw.githubusercontent.com/Homebrew/install/HEAD/install.sh)"
      fi
      # Install .NET Core SDK 2.2 using Homebrew
      brew tap isen-ng/dotnet-sdk-versions
      brew install --cask dotnet-sdk@2.2.207
      # Set environment variables
      export DOTNET_ROOT=$(brew --prefix)/share/dotnet
      export PATH="$DOTNET_ROOT:$PATH"
      # Check the installation
      dotnet version
    SHELL

    # Synced folder for macOS VM
    macos.vm.synced_folder ".", "/Users/vagrant/project"
  end

  config.vm.define "lab5" do |lab5|
    lab5.vm.box = "ubuntu/jammy64"
    lab5.vm.hostname = "lab5-vm"
    lab5.vm.network "public_network"
	lab5.vm.network "forwarded_port", guest: 7128, host: 7128
    lab5.vm.network "forwarded_port", guest: 5165, host: 5165
    lab5.vm.provider "virtualbox" do |vb|
      vb.memory = "8192"
      vb.cpus = 4
    end

# Provisioning for .NET installation on Ubuntu
    lab5.vm.provision "shell", inline: <<-SHELL
      # Update the system
      sudo apt-get update
      sudo apt-get install -y wget apt-transport-https
      # Install the Microsoft GPG key
      wget https://packages.microsoft.com/config/ubuntu/22.04/packages-microsoft-prod.deb
      sudo dpkg -i packages-microsoft-prod.deb
      # Install .NET SDK
      sudo apt-get update
      sudo apt-get install -y dotnet-sdk-8.0
      # Check the installation
      dotnet version

      #dotnet dev-certs https --trust

      #dotnet run --urls "https://0.0.0.0:7128"
    SHELL

    # Synced folder for Linux VM
    lab5.vm.synced_folder ".", "/home/vagrant/project"
  end

    config.vm.define "lab6" do |lab6|
    lab6.vm.box = "ubuntu/jammy64"
    lab6.vm.hostname = "lab6-vm"
    lab6.vm.network "public_network"
    #lab6.vm.network "private_network", ip: "192.168.0.105"
    lab6.vm.provider "virtualbox" do |vb|
      vb.memory = "8192"
      vb.cpus = 4
    end

# Provisioning for .NET installation on Ubuntu
    lab6.vm.provision "shell", inline: <<-SHELL
      # Update the system
      sudo apt-get update
      sudo apt-get install -y wget apt-transport-https
      # Install the Microsoft GPG key
      wget https://packages.microsoft.com/config/ubuntu/22.04/packages-microsoft-prod.deb
      sudo dpkg -i packages-microsoft-prod.deb
      # Install .NET SDK
      sudo apt-get update
      sudo apt-get install -y dotnet-sdk-8.0
      # Check the installation
      dotnet version

    SHELL

    # Synced folder for Linux VM
    lab6.vm.synced_folder ".", "/home/vagrant/project"
  end
end