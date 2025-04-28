using ClubeDaLeitura.Compartilhado;

namespace ClubeDaLeitura.ModuloAmigo;

public interface IRepositorioAmigo : IRepositorio<Amigo>
{
    public bool VerificarTelefoneNovoRegistro(Amigo novoAmigo);
    public bool VerificarTelefoneEditarRegistro(Amigo amigoEscolhido, Amigo dadosEditados);
}
