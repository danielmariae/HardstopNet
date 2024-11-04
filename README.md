# Sistema de E-commerce - HardstopNet

## Descrição

O **HardstopNet** é um sistema de e-commerce desenvolvido em **ASP.NET Framework 4.8.1** com o objetivo de permitir a compra de produtos online. Os usuários podem navegar pelo catálogo de produtos, adicionar itens ao carrinho, realizar o checkout e efetuar o pagamento. Além disso, os usuários podem gerenciar uma lista de produtos favoritos e acompanhar o status dos seus pedidos.

O sistema utiliza o **ASP.NET Identity** para gerenciar a autenticação e o controle de acesso dos usuários, garantindo que cada usuário tenha acesso apenas aos seus próprios pedidos, carrinho e favoritos. 

## Principais Entidades e Relacionamentos

- **Produto**: Representa um produto disponível para venda no sistema. Cada produto possui atributos como nome, descrição, preço e quantidade em estoque. Um produto pode pertencer a várias categorias.

- **Categoria**: Define uma categoria para os produtos, permitindo organizá-los em grupos. A relação entre `Produto` e `Categoria` é muitos-para-muitos.

- **Carrinho**: Associado a um único `Usuario`, o carrinho armazena os produtos selecionados para compra antes do pedido ser finalizado. Cada carrinho pode conter múltiplos `ItemCarrinho`.

- **ItemCarrinho**: Representa um item no carrinho de compras, associando um `Produto` específico com sua quantidade e preço unitário no momento da adição.

- **Pedido**: Contém as informações de uma compra finalizada, incluindo a data/hora do pedido e seu status. O status pode ser "Pendente", "Processando", "Enviado", "Concluído" ou "Cancelado".

- **StatusPedido** (Enumeração): Define os diferentes estados que um pedido pode ter, permitindo o acompanhamento do progresso do pedido pelo usuário.

- **Usuario**: Armazena os dados dos usuários, como nome, e-mail e senha. Cada usuário possui um único carrinho, uma lista de favoritos e pode ter múltiplos pedidos.

- **Favoritos**: Permite que o usuário marque produtos como favoritos para fácil acesso no futuro. Cada `Usuario` possui uma lista de produtos favoritos.

- **Pagamento**: Contém informações sobre o pagamento do pedido, incluindo o valor total, a forma de pagamento e o status de confirmação do pagamento.

### Diagrama de Relacionamentos

- `Usuario` e `Carrinho`: Um usuário possui um carrinho único (`1:1`).
- `Carrinho` e `ItemCarrinho`: Um carrinho pode ter múltiplos itens (`1:N`).
- `Produto` e `ItemCarrinho`: Cada item do carrinho está associado a um único produto (`1:N`).
- `Produto` e `Categoria`: Produtos podem pertencer a várias categorias e vice-versa (`N:N`).
- `Usuario` e `Pedido`: Um usuário pode ter múltiplos pedidos (`1:N`).
- `Pedido` e `StatusPedido`: Cada pedido possui um status único (`1:1`).
- `Pedido` e `Pagamento`: Cada pedido possui um pagamento associado (`1:1`).
- `Usuario` e `Favoritos`: Um usuário possui uma lista de favoritos (`1:1`).
- `Favoritos` e `Produto`: Um favorito pode ter múltiplos produtos e um produto pode ser marcado como favorito por múltiplos usuários (`N:N`).

## Pré-requisitos

Para executar o sistema, é necessário:

1. **.NET Framework 4.8.1** - O sistema foi desenvolvido em **ASP.NET Framework 4.8.1**, portanto, essa versão ou superior é necessária.
2. **SQL Server** - Um banco de dados relacional para armazenar as informações do sistema, como dados dos usuários, produtos, pedidos e pagamentos.
3. **Entity Framework** - Para mapear as classes C# para o banco de dados, simplificando o acesso e manipulação dos dados.
4. **ASP.NET Identity** - Para gerenciar a autenticação e autorização de usuários.
5. **Visual Studio** (recomendado) - Para desenvolvimento e execução da aplicação.

## Execução

1. **Clone o repositório** e abra o projeto no **Visual Studio**.
2. **Configure a conexão com o banco de dados** no arquivo de configuração (`Web.config`), ajustando a `connectionString`.
3. **Realize as migrações** para criar as tabelas no banco de dados com o Entity Framework (utilize `Enable-Migrations` e `Update-Database` no Console do Gerenciador de Pacotes).
4. **Execute o projeto** no Visual Studio (`F5`) para iniciar o servidor de desenvolvimento e acessar a aplicação pelo navegador.

O sistema estará disponível no navegador, onde você poderá registrar usuários, adicionar produtos ao carrinho, realizar pagamentos e visualizar o status dos pedidos.

## OBS: Não se esqueça de adicionar as chaves de autenticação do Email. 
### Passo a Passo para Configurar SMTP com `AddKeys`

1. **Abra o arquivo `Web.config`** na raiz do seu projeto.

2. **Encontre** ou **adicione** a seção `<appSettings>` para armazenar as configurações SMTP.

3. **Insira as configurações de SMTP** como chaves:

```xml
<configuration>
  <!-- Outras configurações do Web.config -->

  <appSettings>
    <!-- Configurações do SMTP -->
    <add key="SmtpHost" value="smtp.seuprovedor.com" />       <!-- Endereço do servidor SMTP -->
    <add key="SmtpPort" value="587" />                         <!-- Porta do servidor SMTP (ex: 587 para TLS) -->
    <add key="SmtpUser" value="seuemail@dominio.com" />        <!-- Seu e-mail para autenticação -->
    <add key="SmtpPassword" value="suaSenhaSegura" />          <!-- Senha do seu e-mail -->
    <add key="SmtpFromAddress" value="seuemail@dominio.com" /> <!-- Endereço "De" para os e-mails -->
  </appSettings>

  <!-- Outras configurações do Web.config -->
</configuration>
```