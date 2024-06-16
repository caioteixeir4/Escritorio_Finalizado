


// Seleciona o formulário pelo seu ID
const form = document.getElementById('meuFormulario');
fetch('https://localhost:7170/Propriedade')
  .then(response => response.json())
  .then(data => {
    const propiedades = data.map(propiedade => {
      return {"id": propiedade.id, "name": propiedade.nome}
    });
    const container = document.getElementById('options');

    propiedades.forEach(propiedade => {
        const option = document.createElement('li');
        option.textContent = propiedade.name;
        option.dataset.value = propiedade.id;
        option.innerText = propiedade.name;
        container.appendChild(option);
    })
    
    const searchBox = document.querySelector('.search-box'); 
    const options = document.querySelectorAll('.options li'); 

    searchBox.addEventListener('input', () => { 
      const searchTerm = searchBox.value.toLowerCase(); 

      options.forEach(option => { 
        const text = option.textContent.toLowerCase(); 
        if (text.includes(searchTerm)) { 
          option.style.display = 'block'; 
        } else { 
          option.style.display = 'none'; 
        } 
      }); 
    }); 
    

for (const option of options) { 
	option.addEventListener('click', () => { 
		const value = option.getAttribute('data-value'); 
    console.log(value)
    document.getElementById('inputList').value = option.textContent
    document.getElementById('inputList').setAttribute('data-value', value);
	}); 
} 
  });

  
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

  // Obtém os valores dos inputs
  const comprovante = document.getElementById('comprovante').value;
  const status = document.getElementById('status').value;
  const valor = document.getElementById('valor').value;
  const referencia = document.getElementById('referencia').value;
  const propriedadeId = document.getElementById('inputList').getAttribute('data-value');

  console.log(comprovante,
    status,
    valor,
    referencia,
    propriedadeId);
  try {
    const responseAddRecibo = await http('POST', 'Recibo', {
      comprovante,
      status,
      valor: Number(valor),
      referencia,
      propriedadeId: Number(propriedadeId)
    })
    if(responseAddRecibo) console.log('Dados enviados com sucesso!');
    else throw new Error(`Falha ao cadastra usuario`);
    window.location.href = '/buscarCliente.html';
  } catch (error) {
    // Exiba uma mensagem de erro amigável para o usuário
    alert('Ocorreu um erro ao enviar os dados. Por favor, tente novamente mais tarde.');
  }
});