# ads-api-locadora
ADS - PABD

## Sobre
API de gerenciamento de filmes para uma locadora.

## Implementação de DbSet com Entity Framework Core

Este projeto foi atualizado para usar Entity Framework Core com acesso a banco de dados MySQL.
As listas estáticas foram substituídas por DbSets, permitindo persistência real dos dados.

## Resolução do erro "Option 'trusted_connection' not supported"

Se você encontrar o erro `System.ArgumentException: Option 'trusted_connection' not supported` ao executar o projeto, siga estas etapas:

### 1. Verificar a string de conexão

O erro ocorre porque a string de conexão contém opções que são específicas do SQL Server e não são suportadas pelo MySQL. A string de conexão correta deve ser:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=Locadora_db;User=root;Password=root;Port=3306;"
}
```

Verifique se o arquivo `appsettings.json` contém esta string de conexão e remova as opções `Trusted_Connection` e `TrustServerCertificate` que não são suportadas pelo MySQL.

### 2. Instalar e configurar o MySQL

Para que a aplicação funcione corretamente, você precisa:

1. **Instalar o MySQL Server**:
   - Baixe e instale o MySQL Server do site oficial: https://dev.mysql.com/downloads/mysql/
   - Durante a instalação, defina a senha "root" para o usuário "root"

2. **Criar o banco de dados após a instalação**:
   - Abra o MySQL Workbench ou outro cliente MySQL
   - Conecte usando usuário "root" e senha "root"
   - Execute o seguinte comando SQL:
     ```sql
     CREATE DATABASE Locadora_db;
     ```

3. **Verificar se o MySQL está em execução**:
   - No Windows, você pode verificar nos Serviços (services.msc)
   - Certifique-se de que o serviço "MySQL" esteja em execução

4. **Testar a conexão**:
   - Você pode testar a conexão usando o MySQL Workbench ou o comando:
     ```
     mysql -u root -p
     ```
   - Digite a senha "root" quando solicitado
   - Se conseguir conectar, execute:
     ```sql
     SHOW DATABASES;
     ```
   - Confirme que "Locadora_db" está na lista

### 3. Aplicar as migrações

Após configurar o MySQL, aplique as migrações para criar o esquema do banco de dados:

```
dotnet ef migrations add InitialCreate
dotnet ef database update
```

### 4. Executar a aplicação

Agora você pode executar a aplicação e acessar o Swagger:

```
dotnet run
```

Acesse o Swagger UI em: http://localhost:5000/swagger ou https://localhost:5001/swagger (a URL exata será mostrada no console ao iniciar a aplicação).

### Como executar as migrações

Para criar e aplicar as migrações do banco de dados, siga os passos abaixo:

1. Instale a ferramenta do EF Core CLI:
```
dotnet tool install --global dotnet-ef
```

2. Adicione a migração inicial:
```
dotnet ef migrations add MigracaoInicial
```

3. Aplique a migração para criar o banco de dados:
```
dotnet ef database update
```

### Próximos passos para integração com MySQL

#### 1. Configuração do MySQL

1. Instale o MySQL Server e MySQL Workbench (se ainda não tiver):
   - Download: https://dev.mysql.com/downloads/installer/
   - Escolha "Custom" durante a instalação e selecione MySQL Server e MySQL Workbench

2. Crie o banco de dados no MySQL Workbench:
   - Abra o MySQL Workbench e conecte-se à sua instância MySQL
   - Clique no ícone "Create a new schema" (ícone de banco de dados com um '+')
   - Digite "Locadora_db" como nome do schema
   - Clique em "Apply" e confirme a criação

3. Verifique se a string de conexão em `appsettings.json` está correta:
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=Locadora_db;User=root;Password=root;Port=3306;"
}
```
   - Substitua `User` e `Password` pelas suas credenciais do MySQL
   - Verifique se a porta está correta (padrão: 3306)

#### 2. Verificando a integração

1. Execute o projeto API:
```
dotnet run
```

2. Use o Swagger para testar os endpoints:
   - Navegue para `https://localhost:5001/swagger` (ou a URL mostrada no console)
   - Crie um novo gênero, estúdio e filme usando os endpoints POST

3. Visualize os dados no MySQL Workbench:
   - Na conexão do MySQL, clique com botão direito em "Tables" e selecione "Refresh All"
   - Você verá tabelas como `filmes`, `generos` e `estudios`
   - Clique com botão direito em uma tabela e selecione "Select Rows - Limit 1000" para visualizar os dados

4. Teste o relacionamento:
   - Crie um gênero usando o endpoint `/generos`
   - Depois crie um filme usando o endpoint `/filmes`, referenciando o ID do gênero criado
   - Verifique no MySQL Workbench que o filme está relacionado ao gênero correto

#### 3. Diagnóstico de problemas comuns

1. Erro de conexão:
   - Verifique se o MySQL Server está rodando
   - Confira as credenciais na string de conexão
   - Teste a conexão no MySQL Workbench

2. Erro de migração:
   - Se houver erros ao executar migrations, verifique o log de erros
   - Em caso de problemas, tente excluir a pasta `Migrations` e recriá-la

3. Dados não aparecendo:
   - Verifique se `SaveChangesAsync()` é chamado após adicionar ou modificar entidades
   - Confirme que o ID usado nas relações está correto

### Comparação das implementações

#### Versão Anterior: Listas Estáticas
- Dados armazenados apenas em memória
- Todos os dados são perdidos quando a aplicação é reiniciada
- Sem suporte a consultas complexas e eficientes

#### Versão Atual: DbSet com Entity Framework
- Persistência real em banco de dados MySQL
- Mapeamento objeto-relacional automático
- Consultas LINQ traduzidas para SQL otimizado
- Relacionamentos gerenciados pelo ORM
- Gerenciamento de esquema através de migrações
