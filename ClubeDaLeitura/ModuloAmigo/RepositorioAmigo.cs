using ClubeDaLeitura.Compartilhado;

namespace ClubeDaLeitura.ModuloAmigo;

public class RepositorioAmigo : RepositorioBase<Amigo>
{
    public bool VerificarTelefoneNovoRegistro(Amigo novoAmigo)
    {
        for (int i = 0; i < Registros.Count; i++)
        {
            if (Registros[i] == null)
                continue;

            if (novoAmigo.Telefone == Registros[i].Telefone && novoAmigo.Id == 0)
                return true;
        }

        return false;
    }
    public bool VerificarTelefoneEditarRegistro(Amigo amigoEscolhido, Amigo dadosEditados)
    {
        for (int i = 0; i < Registros.Count; i++)
        {
            if (Registros[i] == null)
                continue;

            if (dadosEditados.Telefone == Registros[i].Telefone && amigoEscolhido.Id != Registros[i].Id)
                return true;
        }

        return false;
    }
}
