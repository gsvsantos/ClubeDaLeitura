using ClubeDaLeitura.Compartilhado;

namespace ClubeDaLeitura.ModuloEmprestimo;

public interface IRepositorioEmprestimo : IRepositorio<Emprestimo>
{
    public void VerificarEmprestimosAtrasados(List<Emprestimo> emprestimosRegistrados);
    public bool VerificarDevolucao(Emprestimo emprestimoEscolhido);
}
