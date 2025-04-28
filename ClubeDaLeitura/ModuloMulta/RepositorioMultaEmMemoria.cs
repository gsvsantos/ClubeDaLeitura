using ClubeDaLeitura.Compartilhado;
using ClubeDaLeitura.ModuloEmprestimo;

namespace ClubeDaLeitura.ModuloMulta;

public class RepositorioMultaEmMemoria : RepositorioBaseEmMemoria<Multa>, IRepositorioMulta
{
    public List<Multa> PegarListaMultasPendentes()
    {
        List<Multa> multasPendentes = new List<Multa>();

        for (int i = 0; i < Registros.Count; i++)
        {
            if (Registros[i] == null)
                continue;

            if (Registros[i].Status == "Pendente")
                multasPendentes[i] = Registros[i];
        }

        return multasPendentes;
    }
    public bool VerificarMultaExistente(Emprestimo emprestimo)
    {
        foreach (Multa m in emprestimo.Amigo.Multas)
        {
            if (m == null)
                continue;

            if (m.Emprestimo.Id == emprestimo.Id)
                return true;
        }

        return false;
    }
    public bool VerificarMultaQuitada(Multa multaEscolhida)
    {
        if (multaEscolhida == null)
            return false;

        if (multaEscolhida.Status == "Quitada")
            return true;
        else
            return false;
    }
}
