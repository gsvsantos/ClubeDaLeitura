

using ClubeDaLeitura.Compartilhado;

namespace ClubeDaLeitura.ModuloRevista;

public interface IRepositorioRevista : IRepositorio<Revista>
{
    public bool VerificarRevistaEmprestada(Revista revistaEscolhida);
    public bool VerificarRevistaReservada(Revista revistaEscolhida);
    public bool VerificarTituloNovoRegistro(Revista novaRevista);
    public bool VerificarTituloEditarRegistro(Revista revistaEscolhida, Revista dadosEditados);
}
