using ClubeDaLeitura.Compartilhado;
using ClubeDaLeitura.ModuloEmprestimo;

namespace ClubeDaLeitura.ModuloAmigo;

public class RepositorioAmigo : RepositorioBase
{
    public bool VerificarTelefoneNovoRegistro(Amigo novoAmigo)
    {
        for (int i = 0; i < Registros.Length; i++)
        {
            if (Registros[i] == null)
                continue;

            Amigo amigo = (Amigo)Registros[i];

            if (novoAmigo.Telefone == amigo.Telefone && novoAmigo.Id == 0)
                return true;
        }

        return false;
    }
    public bool VerificarTelefoneEditarRegistro(Amigo amigoEscolhido, Amigo dadosEditados)
    {
        for (int i = 0; i < Registros.Length; i++)
        {
            if (Registros[i] == null)
                continue;

            Amigo amigo = (Amigo)Registros[i];

            if (dadosEditados.Telefone == amigo.Telefone && amigoEscolhido.Id != amigo.Id)
                return true;
        }

        return false;
    }
    public bool VerificarEmprestimosAmigo(Amigo amigoEscolhido)
    {
        int emprestimos = 0;

        if (amigoEscolhido.Emprestimos == null)
            return false;

        foreach (Emprestimo e in amigoEscolhido.Emprestimos)
        {
            if (e != null && e.Situacao != "Concluído")
                emprestimos++;
        }

        if (emprestimos > 0)
            return true;
        else
            return false;
    }
}
