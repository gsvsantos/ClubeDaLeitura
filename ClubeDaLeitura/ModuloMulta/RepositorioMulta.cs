using System.Collections;
using ClubeDaLeitura.Compartilhado;
using ClubeDaLeitura.ModuloEmprestimo;

namespace ClubeDaLeitura.ModuloMulta;

public class RepositorioMulta : RepositorioBase
{
    public ArrayList PegarListaMultasPendentes()
    {
        ArrayList multasPendentes = new ArrayList();

        for (int i = 0; i < Registros.Count; i++)
        {
            if (Registros[i] == null)
                continue;

            Multa multa = (Multa)Registros[i]!;

            if (multa.Status == "Pendente")
                multasPendentes[i] = multa;
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
