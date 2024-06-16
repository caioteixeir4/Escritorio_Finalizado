document.getElementById('validarLogin').addEventListener('click', function(event) {
    event.preventDefault();

    const validarUsuario = document.querySelector(".email").value;
    const validarSenha = document.querySelector(".senha").value;

    fetch('https://localhost:7170/Login')
        .then(res => {
            if (!res.ok) {
                throw new Error('Erro ao acessar a API');
            }
            return res.json();
        })
        .then((usuarios) => {
            let usuarioValido = false;

            usuarios.forEach((usuario) => {
                // Comparação exata do email (case-sensitive) e senha (case-sensitive)
                if (usuario.usuario === validarUsuario && usuario.senha === validarSenha) {
                    usuarioValido = true;
                }
            });

            if (usuarioValido) {
                window.location.href = 'buscarCliente.html';
            } else {
                console.log('Usuário inválido');
                alert('Email ou senha inválidos. Por favor, tente novamente.');
            }
        })
        .catch(error => {
            console.error('Erro ao acessar a API:', error);
            alert('Ocorreu um erro ao acessar a API. Por favor, tente novamente mais tarde.');
        });
});
