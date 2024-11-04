# Sistema de E-commerce - HardstopNet

## Descri��o

O **HardstopNet** � um sistema de e-commerce desenvolvido em **ASP.NET Framework 4.8.1** com o objetivo de permitir a compra de produtos online. Os usu�rios podem navegar pelo cat�logo de produtos, adicionar itens ao carrinho, realizar o checkout e efetuar o pagamento. Al�m disso, os usu�rios podem gerenciar uma lista de produtos favoritos e acompanhar o status dos seus pedidos.

O sistema utiliza o **ASP.NET Identity** para gerenciar a autentica��o e o controle de acesso dos usu�rios, garantindo que cada usu�rio tenha acesso apenas aos seus pr�prios pedidos, carrinho e favoritos. 

## Principais Entidades e Relacionamentos

- **Produto**: Representa um produto dispon�vel para venda no sistema. Cada produto possui atributos como nome, descri��o, pre�o e quantidade em estoque. Um produto pode pertencer a v�rias categorias.

- **Categoria**: Define uma categoria para os produtos, permitindo organiz�-los em grupos. A rela��o entre `Produto` e `Categoria` � muitos-para-muitos.

- **Carrinho**: Associado a um �nico `Usuario`, o carrinho armazena os produtos selecionados para compra antes do pedido ser finalizado. Cada carrinho pode conter m�ltiplos `ItemCarrinho`.

- **ItemCarrinho**: Representa um item no carrinho de compras, associando um `Produto` espec�fico com sua quantidade e pre�o unit�rio no momento da adi��o.

- **Pedido**: Cont�m as informa��es de uma compra finalizada, incluindo a data/hora do pedido e seu status. O status pode ser "Pendente", "Processando", "Enviado", "Conclu�do" ou "Cancelado".

- **StatusPedido** (Enumera��o): Define os diferentes estados que um pedido pode ter, permitindo o acompanhamento do progresso do pedido pelo usu�rio.

- **Usuario**: Armazena os dados dos usu�rios, como nome, e-mail e senha. Cada usu�rio possui um �nico carrinho, uma lista de favoritos e pode ter m�ltiplos pedidos.

- **Favoritos**: Permite que o usu�rio marque produtos como favoritos para f�cil acesso no futuro. Cada `Usuario` possui uma lista de produtos favoritos.

- **Pagamento**: Cont�m informa��es sobre o pagamento do pedido, incluindo o valor total, a forma de pagamento e o status de confirma��o do pagamento.

### Diagrama de Relacionamentos

- `Usuario` e `Carrinho`: Um usu�rio possui um carrinho �nico (`1:1`).
- `Carrinho` e `ItemCarrinho`: Um carrinho pode ter m�ltiplos itens (`1:N`).
- `Produto` e `ItemCarrinho`: Cada item do carrinho est� associado a um �nico produto (`1:N`).
- `Produto` e `Categoria`: Produtos podem pertencer a v�rias categorias e vice-versa (`N:N`).
- `Usuario` e `Pedido`: Um usu�rio pode ter m�ltiplos pedidos (`1:N`).
- `Pedido` e `StatusPedido`: Cada pedido possui um status �nico (`1:1`).
- `Pedido` e `Pagamento`: Cada pedido possui um pagamento associado (`1:1`).
- `Usuario` e `Favoritos`: Um usu�rio possui uma lista de favoritos (`1:1`).
- `Favoritos` e `Produto`: Um favorito pode ter m�ltiplos produtos e um produto pode ser marcado como favorito por m�ltiplos usu�rios (`N:N`).

## Pr�-requisitos

Para executar o sistema, � necess�rio:

1. **.NET Framework 4.8.1** - O sistema foi desenvolvido em **ASP.NET Framework 4.8.1**, portanto, essa vers�o ou superior � necess�ria.
2. **SQL Server** - Um banco de dados relacional para armazenar as informa��es do sistema, como dados dos usu�rios, produtos, pedidos e pagamentos.
3. **Entity Framework** - Para mapear as classes C# para o banco de dados, simplificando o acesso e manipula��o dos dados.
4. **ASP.NET Identity** - Para gerenciar a autentica��o e autoriza��o de usu�rios.
5. **Visual Studio** (recomendado) - Para desenvolvimento e execu��o da aplica��o.

## Execu��o

1. **Clone o reposit�rio** e abra o projeto no **Visual Studio**.
2. **Configure a conex�o com o banco de dados** no arquivo de configura��o (`Web.config`), ajustando a `connectionString`.
3. **Realize as migra��es** para criar as tabelas no banco de dados com o Entity Framework (utilize `Enable-Migrations` e `Update-Database` no Console do Gerenciador de Pacotes).
4. **Execute o projeto** no Visual Studio (`F5`) para iniciar o servidor de desenvolvimento e acessar a aplica��o pelo navegador.

O sistema estar� dispon�vel no navegador, onde voc� poder� registrar usu�rios, adicionar produtos ao carrinho, realizar pagamentos e visualizar o status dos pedidos.

## OBS: N�o se esque�a de adicionar as chaves de autentica��o do Email. 
### Passo a Passo para Configurar SMTP com `AddKeys`

1. **Abra o arquivo `Web.config`** na raiz do seu projeto.

2. **Encontre** ou **adicione** a se��o `<appSettings>` para armazenar as configura��es SMTP.

3. **Insira as configura��es de SMTP** como chaves:

```xml
<configuration>
  <!-- Outras configura��es do Web.config -->

  <appSettings>
    <!-- Configura��es do SMTP -->
    <add key="SmtpHost" value="smtp.seuprovedor.com" />       <!-- Endere�o do servidor SMTP -->
    <add key="SmtpPort" value="587" />                         <!-- Porta do servidor SMTP (ex: 587 para TLS) -->
    <add key="SmtpUser" value="seuemail@dominio.com" />        <!-- Seu e-mail para autentica��o -->
    <add key="SmtpPassword" value="suaSenhaSegura" />          <!-- Senha do seu e-mail -->
    <add key="SmtpFromAddress" value="seuemail@dominio.com" /> <!-- Endere�o "De" para os e-mails -->
  </appSettings>

  <!-- Outras configura��es do Web.config -->
</configuration>
```