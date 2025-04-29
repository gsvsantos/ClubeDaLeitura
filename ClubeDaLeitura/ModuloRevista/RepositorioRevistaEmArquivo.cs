

using ClubeDaLeitura.Compartilhado;

namespace ClubeDaLeitura.ModuloRevista;

public class RepositorioRevistaEmArquivo : RepositorioBaseEmArquivo<Revista>, IRepositorioRevista
{
    public RepositorioRevistaEmArquivo(ContextoDados contexto) : base(contexto) { }
    public override void CadastrarRegistro(Revista novoRegistro)
    {
        novoRegistro.Caixa.AdicionarRevista(novoRegistro);
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

            if (novaRevista.Titulo == Registros[i].Titulo && novaRevista.NumeroEdicao == Registros[i].NumeroEdicao && novaRevista.Id == 0)
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

            if (dadosEditados.Titulo == Registros[i].Titulo && dadosEditados.NumeroEdicao == Registros[i].NumeroEdicao && revistaEscolhida.Id != Registros[i].Id)
                return true;
        }

        return false;
    }
    protected override List<Revista> ObterListaContexto()
    {
        return Contexto.RegistroRevistas;
    }
}
