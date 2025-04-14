
namespace ClubeDaLeitura.ModuloRevista;

public class RepositorioRevista
{
    public Revista[] Revistas = new Revista[100];
    public int IndiceListaRevista = 0;
    public bool ListaVazia = false;

    public void RegistrarRevista(Revista novaRevista)
    {
        novaRevista.GerarId();
        novaRevista.Caixa.AdicionarRevista(novaRevista);
        Revistas[IndiceListaRevista++] = novaRevista;
    }
    public Revista[] PegarListaRegistrados()
    {
        return Revistas;
    }
    public void EditarRevista(Revista revistaEscolhida, Revista dadosEditados)
    {
        revistaEscolhida.Caixa.RemoverRevista(revistaEscolhida);
        dadosEditados.Caixa.AdicionarRevista(revistaEscolhida);
        revistaEscolhida.Caixa = dadosEditados.Caixa;
        revistaEscolhida.Titulo = dadosEditados.Titulo;
        revistaEscolhida.NumeroEdicao = dadosEditados.NumeroEdicao;
        revistaEscolhida.AnoPublicacao = dadosEditados.AnoPublicacao;
    }
    public void ExcluirRevista(Revista revistaEscolhida)
    {
        revistaEscolhida.Caixa.RemoverRevista(revistaEscolhida);

        for (int i = 0; i < Revistas.Length; i++)
        {
            if (Revistas[i] == null)
                continue;
            else if (Revistas[i].Id == revistaEscolhida.Id)
            {
                Revistas[i] = null!;
                break;
            }
        }
    }
    public Revista SelecionarPorId(int idRevistaEscolhida)
    {
        foreach (Revista r in Revistas)
        {
            if (r == null)
                continue;

            if (r.Id == idRevistaEscolhida)
                return r;
        }

        return null!;
    }
    public bool VerificarRevistaDisponivel(Revista revistaEscolhida)
    {
        if (revistaEscolhida.StatusEmprestimo == "Disponível")
            return true;
        else
            return false;
    }
    public bool VerificarRevistaEmprestada(Revista revistaEscolhida)
    {
        if (revistaEscolhida.StatusEmprestimo == "Emprestada")
            return true;
        else
            return false;
    }
    public bool VerificarTituloNovoRegistro(Revista novaRevista)
    {
        for (int i = 0; i < Revistas.Length; i++)
        {
            if (Revistas[i] == null)
                continue;

            if (novaRevista.Titulo == Revistas[i].Titulo && novaRevista.NumeroEdicao == Revistas[i].NumeroEdicao && novaRevista.Id == 0)
                return true;
        }

        return false;
    }
    public bool VerificarTituloEditarRegistro(Revista revistaEscolhida, Revista dadosEditados)
    {
        for (int i = 0; i < Revistas.Length; i++)
        {
            if (Revistas[i] == null)
                continue;

            if (dadosEditados.Titulo == Revistas[i].Titulo && dadosEditados.NumeroEdicao == Revistas[i].NumeroEdicao && revistaEscolhida.Id != Revistas[i].Id)
                return true;
        }

        return false;
    }
}
