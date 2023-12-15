# MoneySenseWeb


Se o seu projeto já está pronto e você deseja adicionar a migração, criar o banco de dados e aplicar a migração para atualizá-lo, você pode seguir estes passos:

1. Certifique-se de ter o Entity Framework Core instalado:
Se ainda não tiver, execute os seguintes comandos no terminal:

dotnet add package Microsoft.EntityFrameworkCore
dotnet add package Microsoft.EntityFrameworkCore.Tools
dotnet tool install --global dotnet-ef --version 7.0.14 (Importante a versão)

2. Certifique-se de ter a string de conexão configurada:
Verifique se a string de conexão com o banco de dados está configurada corretamente no arquivo appsettings.json

3. Navegue até o diretório do projeto no terminal:

4. Aplique a migração para criar o banco de dados:

dotnet ef database update
