using ClubeDaLeitura.Compartilhado;
using ClubeDaLeitura.ModuloRevista;

namespace ClubeDaLeitura.ModuloCaixa;
public class RepositorioCaixa : RepositorioBase
{
    public bool VerificarEtiquetas(Caixa caixaVerificar)
    {
        for (int i = 0; i < Registros.Length; i++)
        {
            if (Registros[i] == null)
                continue;

            Caixa caixa = (Caixa)Registros[i];

            if (caixaVerificar.Etiqueta == caixa.Etiqueta)
                return true;
        }

        return false;
    }
    public bool VerificarRevistasCaixa(Caixa caixaEscolhida)
    {
        int revistas = 0;

        if (caixaEscolhida.Revistas == null)
            return false;

        foreach (Revista r in caixaEscolhida.Revistas)
        {
            if (r != null)
                revistas++;
        }

        if (revistas > 0)
            return true;
        else
            return false;
    }
}
