using System.Collections;
using ClubeDaLeitura.Compartilhado;
using ClubeDaLeitura.ModuloAmigo;

namespace ClubeDaLeitura.ModuloEmprestimo;

public class RepositorioEmprestimo : RepositorioBase
{
    public override void CadastrarRegistro(EntidadeBase novoRegistro)
    {
        Emprestimo novoEmprestimo = (Emprestimo)novoRegistro;

        novoEmprestimo.Revista.Emprestar();
        novoEmprestimo.Amigo.ReceberEmprestimo(novoEmprestimo);
        base.CadastrarRegistro(novoEmprestimo);
    }
    public bool VerificarEmprestimoAtivo(Amigo amigoEscolhido)
    {
        if (amigoEscolhido.Emprestimos == null)
            return false;

        foreach (Emprestimo e in amigoEscolhido.Emprestimos)
        {
            if (e == null)
                continue;

            if (e.Situacao == "Aberto" || e.Situacao == "ATRASADO")
                return true;
        }

        return false;
    }
    public void VerificarEmprestimosAtrasados(ArrayList emprestimosRegistrados)
    {
        foreach (Emprestimo e in emprestimosRegistrados)
        {
            if (e == null)
                continue;

            if (e.Situacao == "Concluído")
                continue;

            if (DateTime.Now > e.ObterDataDevolucao())
                e.Situacao = "ATRASADO";
        }
    }
    public bool VerificarDevolucao(Emprestimo emprestimoEscolhido)
    {
        for (int i = 0; i < Registros.Count; i++)
        {
            if (Registros[i] == null)
                continue;

            Emprestimo emprestimo = (Emprestimo)Registros[i]!;

            if (emprestimoEscolhido.Id == emprestimo.Id && emprestimoEscolhido.Situacao == "Concluído")
                return true;
        }

        return false;
    }
}
