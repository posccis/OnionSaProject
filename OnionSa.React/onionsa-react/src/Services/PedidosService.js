class PedidosService{
    async RetornaPedidos(){
        return new Promise((resolve, reject) => {
            fetch('https://localhost:44309/onionsa/Pedidos/obter-dados').then(async (response) => {
                if(response.ok){
                    resolve(response.json());
                }else{
                    throw new Error(response.Error.message);
                }
            }).then((data) => {

            }).catch((error) => {
                reject(error);
            })
        })
    };
    async RetornaPorRegiao(dados){
        return new Promise((resolve, reject) => {
            const counts = {};

            dados.forEach(pedido => {
              const regiao = pedido.regiao;
          
              // Se a região ainda não estiver no objeto de contagem, inicialize-a com 1; caso contrário, incremente a contagem.
              counts[regiao] = counts[regiao] ? counts[regiao]+ 1 : 1;
            });
            resolve(counts)
        })
    }
    async RetornaPorProduto(dados){
        return new Promise((resolve, reject) => {
            const counts = {};

            dados.forEach(pedido => {
              const produto = pedido.produto;
          
              // Se a região ainda não estiver no objeto de contagem, inicialize-a com 1; caso contrário, incremente a contagem.
              counts[produto] = counts[produto] ? counts[produto]+ 1 : 1;
            });
            resolve(counts)
        })
    }
}

export default new PedidosService;