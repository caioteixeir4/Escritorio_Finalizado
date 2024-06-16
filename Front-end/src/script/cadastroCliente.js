// Seleciona o formulário pelo seu ID
const form = document.getElementById('meuFormulario');

// Adiciona um evento de submit ao formulário
form.addEventListener('submit', async function(event) {
  // Previne o comportamento padrão do formulário (recarregar a página)
  event.preventDefault();

 
  async function http(method, url, payload) {
    try {
      const response = await fetch('https://localhost:7170/' + url, {
        method: method,
        headers: {
          'Content-Type': 'application/json'
        },
        body: method !== 'GET' && method !== 'HEAD' ? JSON.stringify(payload) : null
      });

      if (!response.ok) {
        throw new Error(`HTTP error! status: ${response.status}`);
      }

      const data = await response.json();
      console.log('cabo', 'https://localhost:7170/' + url);
      return data;
    } catch (error) {
      return null;
    }
  };

  async function getCidadeId(cidade, ddd) {
    const getCidadeResponse = await http('GET', 'Cidade/' + cidade, null);
    if(getCidadeResponse) return getCidadeResponse.id
    const cidadeResponse = await http('POST', 'Cidade',  {
        nome: cidade,
        ddd: ddd
      }
    );
    return cidadeResponse.id;
  };

  async function getEnderecoId(rua, bairro, cep, cidade, ddd) {
    const getEnderecoResponse = await http('GET', 'Endereco/' + rua, null);
    if(getEnderecoResponse) return getEnderecoResponse.id
    const cidadeId = await getCidadeId(cidade, ddd);
    const enderecoResponse = await http('POST', 'Endereco', {
      cidadeId,
      bairro,
      rua,
      cep
    });
    return enderecoResponse.id;
  };

  // Obtém os valores dos inputs
  const nomeCliente = document.getElementById('nomeCliente').value;
  const cpf = document.getElementById('cpf').value;
  const rg = document.getElementById('rg').value;
  const celular = document.getElementById('celular').value;
  const rua = document.getElementById('rua').value;
  const bairro = document.getElementById('bairro').value;
  const cep = document.getElementById('cep').value;
  const cidade = document.getElementById('cidade').value;
  const ddd = document.getElementById('ddd').value;

  try {
    // Envia os dados para a API de Cidade
    const enderecoId = await getEnderecoId(rua, bairro, cep, cidade, ddd);

    const responseAddUser = await http('POST', 'Cliente', {
      nome: nomeCliente,
      cpf,
      rg,
      celular,
      enderecoId
    })

    if(responseAddUser) console.log('Dados enviados com sucesso!');
    else throw new Error(`Falha ao cadastra usuario`);
    window.location.href = '/buscarCliente.html';
  } catch (error) {
    console.error('Erro ao enviar os dados:', error);
    // Exiba uma mensagem de erro amigável para o usuário
    alert('Ocorreu um erro ao enviar os dados. Por favor, tente novamente mais tarde.');
  }
});