using ClubeDaLeitura.Compartilhado;
using ClubeDaLeitura.ModuloEmprestimo;

namespace ClubeDaLeitura.ModuloMulta;

public interface IRepositorioMulta : IRepositorio<Multa>
{
    public List<Multa> PegarListaMultasPendentes();
    public bool VerificarMultaExistente(Emprestimo emprestimo);
    public bool VerificarMultaQuitada(Multa multaEscolhida);
}
