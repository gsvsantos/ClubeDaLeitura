using ClubeDaLeitura.Compartilhado;

namespace ClubeDaLeitura.ModuloAmigo;

public class RepositorioAmigo : RepositorioBase
{
    public bool VerificarTelefoneNovoRegistro(Amigo novoAmigo)
    {
        for (int i = 0; i < Registros.Count; i++)
        {
            if (Registros[i] == null)
                continue;

            Amigo amigo = (Amigo)Registros[i]!;

            if (novoAmigo.Telefone == amigo.Telefone && novoAmigo.Id == 0)
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

            Amigo amigo = (Amigo)Registros[i]!;

            if (dadosEditados.Telefone == amigo.Telefone && amigoEscolhido.Id != amigo.Id)
                return true;
        }

        return false;
    }
}
