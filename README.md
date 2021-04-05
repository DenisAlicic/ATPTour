# ATPTour

***
## :package: Installation
:exclamation: Requirements: .NET Core, Angular, SQL Server, Azure Data Studio, Docker

1. Install .NET Core
    ```sh
    sudo pacman -S dotnet-sdk aspnet-runtime
    export DOTNET_ROOT=/usr/share/dotnet
    export MSBuildSDKsPath=$DOTNET_ROOT/sdk/$(${DOTNET_ROOT}/dotnet --version)/Sdks
    export PATH=${PATH}:${DOTNET_ROOT}

    ```

2. Install Angular
    ```sh
    cd Client
    sudo npm install -g @angular/cli
    sudo npm install

    ```

3. SQL Server (at least 2019)
    ```sh
    docker run --name sql_server -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=<password>' -p 1433:1433 -d mcr.microsoft.com/mssql/server
    mssql -u <username> -p <password>   
    select @@version

    ```

### Manual

1. Clone this repository somewhere on your machine.

    ```sh
    git clone https://github.com/DenisAlicic/TennisAssociation/tree/develop/TennisAssociation

    ```
2. Start (front-end)

    ```sh
    cd Client
    ng serve

    ```

3. Start app

    ```sh
    dotnet build
    dotnet run --urls "http://localhost:8080"   

    ```

### Create migrations


    ```sh
    dotnet tool install --global dotnet-ef
    add ~/.dotnet/tools in path

    dotnet ef migrations add Initial --context TennisAssociationIdentityContext  
    dotnet ef database update --context TennisAssociationIdentityContext  

    ```
