## 🚀 FrontEnd NET Consumir JWT
Exemplo de criação CRUD de Consumo de WebAPI com autorização utilizando JWT, focadas em arquitetura e segurança.

### O que você vai encontrar neste projeto
| Tecnologia | Descrição |
|-----------|-----------|
| **JWT** | Uso de autorização em WebAPI |
| **Razor** | Atua como o motor de renderização que combina C# com HTML para criar páginas dinâmicas |
| **Service** | Prática utilizada com Interfaces e Injeção de Dependência para Arquitetura em Camadas (ou Clean Architecture ) |

### Execução da aplicação
Executa a aplicação Backend **https://github.com/Marcelofazan/API-EF10-JWT** que se encontra no Github.

  - [API-EF10-JWT](https://github.com/Marcelofazan/API-EF10-JWT)
  
O banco de dados é SQLite **(`SistemaERPOnlineForcaDeVendasAPI.db`)** , onde será maninupado por essa aplicação como Frontend.

#### Execução Inicial de Endpoints (Postman)
**(dadosadmin.json)**
- Existe um arquivo de configuração para registrar onde será executado pelo Front End esse caminho **https://localhost:7092/api/auth/registro**

   ```json
	{ 
    		"idempresa": 1, 
    		"email": "email@email.com", 
    		"senha": "123456", 
    		"nome": "Usuario", 
    		"taxapercentual": 10.00
	}
   ```

### Rotas dos métodos 
| Tecnologia | Descrição |
|-----------|-----------|
| **Metodo: GET/POST /api/Produtos** | Função: Listar / Criar produtos |JWT: Sim |
| **Metodo: GET/PUT/DELETE /api/Produtos/{id}** | Função: Obter / Atualizar / Excluir produto | JWT: Sim |

