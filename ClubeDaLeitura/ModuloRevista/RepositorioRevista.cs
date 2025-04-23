

using ClubeDaLeitura.Compartilhado;

namespace ClubeDaLeitura.ModuloRevista;

public class RepositorioRevista : RepositorioBase
{
    public override void CadastrarRegistro(EntidadeBase novoRegistro)
    {
        Revista novaRevista = (Revista)novoRegistro;

        novaRevista.Caixa.AdicionarRevista(novaRevista);
        base.CadastrarRegistro(novoRegistro);
    }
    public bool VerificarRevistaEmprestada(Revista revistaEscolhida)
    {
        if (revistaEscolhida.StatusEmprestimo == "Emprestada")
            return true;
        else
            return false;
    }
    public bool VerificarRevistaReservada(Revista revistaEscolhida)
    {
        if (revistaEscolhida.StatusEmprestimo == "Reservada")
            return true;
        else
            return false;
    }
    public bool VerificarTituloNovoRegistro(Revista novaRevista)
    {
        for (int i = 0; i < Registros.Count; i++)
        {
            if (Registros[i] == null)
                continue;

            Revista revista = (Revista)Registros[i]!;

            if (novaRevista.Titulo == revista.Titulo && novaRevista.NumeroEdicao == revista.NumeroEdicao && novaRevista.Id == 0)
                return true;
        }

        return false;
    }
    public bool VerificarTituloEditarRegistro(Revista revistaEscolhida, Revista dadosEditados)
    {
        for (int i = 0; i < Registros.Count; i++)
        {
            if (Registros[i] == null)
                continue;

            Revista revista = (Revista)Registros[i]!;

            if (dadosEditados.Titulo == revista.Titulo && dadosEditados.NumeroEdicao == revista.NumeroEdicao && revistaEscolhida.Id != revista.Id)
                return true;
        }

        return false;
    }
}
