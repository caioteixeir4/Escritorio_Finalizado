let clientes;
fetch('https://localhost:7170/Cliente')
  .then(response => response.json())
  .then(data => {
    clientes = data.filter(client => client.propriedades.length)
    exibirDadosNaTabela(clientes);

    searchInput.addEventListener("input", function() {
      const searchTerm = this.value.toLowerCase();

      fetch("https://localhost:7170/Cliente")
        .then(response => response.json())
        .then(data => {
          const filteredData = data.filter(client => 
            client.propriedades.length && (
            client.nome.toLowerCase().includes(searchTerm) ||
            client.propriedades.filter(propriedade => propriedade.numPasta.toString().includes(searchTerm)).length)
          );

          updateSuggestionList(filteredData);
        })
        .catch(error => {
          console.error("Erro ao obter os dados:", error);
        });
    });
  })
  .catch(error => {
    console.error('Erro ao obter os dados:', error);
  });

async function excluirPropiedade(id){
  const responseRecibo = await fetch('https://localhost:7170/Recibo/', {
    method: 'GET',
    headers: {
      'Content-Type': 'application/json'
    },
  })
  .then(data => data.json())
  .catch(err => null);
  const recibo = responseRecibo.find(recibo => recibo.id === id);
  if(recibo) await fetch('https://localhost:7170/Recibo/'+recibo.id, {
    method: 'DELETE',
    headers: {
      'Content-Type': 'application/json'
    },
  })
  .then(data => data.json())
  .catch(err => null);

  const response = await fetch('https://localhost:7170/Propriedade/'+id, {
    method: 'DELETE',
    headers: {
      'Content-Type': 'application/json'
    },
  })
  .then(data => data.json())
  .catch(err => null);
}

function preencheModal(cliente, propriedade) {
  const searchInput = document.querySelector(".form-control");
  document.getElementById('nomeCliente').value = cliente.nome;
  document.getElementById('cpfCliente').value = cliente.cpf;
  document.getElementById('rgCliente').value = cliente.rg;
  document.getElementById('celularCliente').value = cliente.celular;
  
  document.getElementById('nomePropriedade').value = propriedade.nome;
  document.getElementById('cnpjPropriedade').value = propriedade.cnpj;
  document.getElementById('incricaoPropriedade').value = propriedade.inscricaoEstadual;
  document.getElementById('statusPropriedade').value = propriedade.status;
  document.getElementById('numPastaPropriedade').value = propriedade.numPasta;

  const select = document.getElementById('selectPropriedade');
  select.addEventListener('change', () => {
    console.log(document.getElementById('selectPropriedade').value, cliente.propriedades)
    const idPropriedade = Number(document.getElementById('selectPropriedade').value);
    const propriedade = cliente.propriedades.find(propriedade => propriedade.id === idPropriedade);
    document.getElementById('nomePropriedade').value = propriedade.nome;
    document.getElementById('cnpjPropriedade').value = propriedade.cnpj;
    document.getElementById('incricaoPropriedade').value = propriedade.inscricaoEstadual;
    document.getElementById('statusPropriedade').value = propriedade.status;
    document.getElementById('numPastaPropriedade').value = propriedade.numPasta;


  })
  cliente.propriedades.forEach(propriedade => {
    const option = document.createElement('option');
    option.value = propriedade.id;
    option.textContent = propriedade.nome;
    select.appendChild(option);
  })
  select.selectedIndex = propriedade.numPasta;

  
  const alterar = document.getElementById('alterar');
  alterar.addEventListener('click', async () => {
    const nome = document.getElementById('nomePropriedade').value;
    const cnpj = document.getElementById('cnpjPropriedade').value;
    const inscricaoEstadual = document.getElementById('incricaoPropriedade').value;
    const status = document.getElementById('statusPropriedade').value;
    const numPasta = document.getElementById('numPastaPropriedade').value;
    console.log(propriedade);
    const response = await fetch('https://localhost:7170/Propriedade', {
      method: 'PUT',
      headers: {
        'Content-Type': 'application/json'
      },
      body: JSON.stringify({
        nome,
        cnpj,
        inscricaoEstadual,
        status,
        numPasta,
        enderecoId: propriedade.enderecoId,
        clienteId: propriedade.clienteId,
        id: propriedade.id,
      })
    })
    .then(data => data.json())
    .catch(err => null);
    if(response) $('#modal').modal('hide');
  })
  
  const excluir = document.getElementById('excluir');
  excluir.addEventListener('click', async () => {    
    const response = await excluirPropiedade(propriedade.id);
    if(response) $('#modal').modal('hide');
  })
  
  const excluirCliente = document.getElementById('excluirCliente');
  excluirCliente.addEventListener('click', async () => {    
  await excluirPropiedade(propriedade.id);
  
  const response = await fetch('https://localhost:7170/Cliente/'+cliente.id, {
    method: 'DELETE',
    headers: {
      'Content-Type': 'application/json'
    },
  })
  .then(data => data.json())
  .catch(err => null);
    if(response) $('#modal').modal('hide');
  })
}
function exibirDadosNaTabela(clientes) {
  const tabelaClientes = document.querySelector('.tabela__inicial__buscarCliente tbody');
  tabelaClientes.innerHTML = ''; // Limpa a tabela antes de adicionar os novos dados

  clientes.forEach(cliente => {
    cliente.propriedades.forEach(propriedade => {
      const row = document.createElement('tr');
  
      const idCell = document.createElement('td');
      idCell.textContent = propriedade.numPasta; // Usa o campo 'numPasta' da primeira propriedade
      row.appendChild(idCell);
  
      const nomeCell = document.createElement('td');
      const nomeLink = document.createElement('a');
      nomeLink.textContent = cliente.nome;
      nomeLink.href = '#'; // Pode ser um link vazio por enquanto
      nomeLink.addEventListener('click', function() {
        preencheModal(cliente, propriedade);
        $('#modal').modal('show');
      });
      nomeCell.appendChild(nomeLink);
      row.appendChild(nomeCell);
  
      tabelaClientes.appendChild(row);
    })
  });
}
function updateSuggestionList(filteredData) {
  const tabelaClientes = document.querySelector('.tabela__inicial__buscarCliente tbody');
  tabelaClientes.innerHTML = ''; // Limpa a tabela antes de adicionar os novos dados

  filteredData.forEach(cliente => {
    const row = document.createElement('tr');

    const idCell = document.createElement('td');
    idCell.textContent = cliente.propriedades[0].numPasta;
    row.appendChild(idCell);

    const nomeCell = document.createElement('td');
    nomeCell.textContent = cliente.nome;
    row.appendChild(nomeCell);

    tabelaClientes.appendChild(row);
  });
}


const permitirAlterar = document.getElementById('permitirAlterar');
permitirAlterar.addEventListener('click', () => {
  document.getElementById('nomePropriedade').removeAttribute('disabled');
  document.getElementById('cnpjPropriedade').removeAttribute('disabled');
  document.getElementById('incricaoPropriedade').removeAttribute('disabled');
  document.getElementById('statusPropriedade').removeAttribute('disabled');
  document.getElementById('numPastaPropriedade').removeAttribute('disabled');
})
