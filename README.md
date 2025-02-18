# 📌 Durable Functions - Processamento de Pedidos

Este projeto demonstra o uso de **Azure Durable Functions** no .NET 8 para orquestrar um fluxo de processamento de pedidos.

## 🚀 Visão Geral
O fluxo é composto por três fases principais:
1. **Validação do Pedido** 🛠️
2. **Processamento do Pagamento** 💳
3. **Envio de Confirmação** 📩

Se uma fase falhar, o sistema permite **reativar a execução a partir do ponto de falha** sem perder o progresso.

---

## 🏗️ Estrutura do Projeto
```
📂 DurableFunctionsOrder
│── 📂 OrderFunctions            # Funções HTTP de entrada
│   ├── StartOrderProcess.cs     # Inicia a orquestração
│── 📂 OrderOrchestrator         # Orquestrador principal
│   ├── OrderOrchestrator.cs     # Define o fluxo do pedido
│── 📂 OrderActivities           # Atividades executadas pela orquestração
│   ├── ValidateOrder.cs         # Valida o pedido
│   ├── ProcessPayment.cs        # Processa pagamento
│   ├── SendConfirmation.cs      # Envia confirmação ao cliente
│── host.json                    # Configuração do Azure Functions
│── local.settings.json          # Configuração local
│── README.md                    # Documentação
```

---

## 🛠️ Como Rodar Localmente
### 1️⃣ Instalar Dependências
Certifique-se de ter:
- .NET 8
- Azure Functions Core Tools
- Armazenamento (Azure Storage Emulator ou Azurite)

### 2️⃣ Criar o Projeto Durable Functions (se ainda não criou)
```sh
dotnet new func --worker-runtime dotnet-isolated
```

### 3️⃣ Rodar a Aplicação
```sh
dotnet build
func start
```
> A aplicação rodará em `http://localhost:7071`

---

## 🎯 Como Usar a API

### 🔹 **Iniciar a Orquestração**
```sh
curl -X POST http://localhost:7071/api/StartOrderProcess
```
📌 **Retorno esperado:**
```json
{
  "message": "Orquestração iniciada com ID: {instanceId}"
}
```

### 🔹 **Consultar Status da Orquestração**
```sh
curl -X GET http://localhost:7071/runtime/webhooks/durabletask/instances/{instanceId}
```

### 🔹 **Reiniciar Orquestração do Ponto de Falha**
```sh
curl -X POST -H "Content-Type: application/json" \
     -d '{ "reason": "Corrigido problema no pagamento" }' \
     http://localhost:7071/runtime/webhooks/durabletask/instances/{instanceId}/rewind
```

### 🔹 **Enviar Evento Externo para Continuar o Fluxo**
```sh
curl -X POST -H "Content-Type: application/json" \
     -d "true" \
     http://localhost:7071/runtime/webhooks/durabletask/instances/{instanceId}/raiseEvent/PaymentApproved
```

---

## 📌 Conclusão
Esse projeto demonstra o uso de **Azure Durable Functions** para criar fluxos de trabalho resilientes e escaláveis no .NET 8. A orquestração mantém o estado automaticamente, permitindo retentativas, pausas e retomadas de processos sem perder o progresso.

🔹 **Dúvidas ou melhorias?** Sinta-se à vontade para contribuir ou relatar issues! 🚀

