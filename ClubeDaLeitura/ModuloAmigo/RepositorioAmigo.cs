using ClubeDaLeitura.ModuloEmprestimo;

namespace ClubeDaLeitura.ModuloAmigo;

public class RepositorioAmigo
{
    public Amigo[] Amigos = new Amigo[100];
    public int IndiceListaAmigos = 0;
    public bool ListaVazia = false;

    public void RegistrarAmigo(Amigo novoAmigo)
    {
        novoAmigo.GerarId();
        Amigos[IndiceListaAmigos++] = novoAmigo;
    }
    public Amigo[] PegarListaRegistrados()
    {
        return Amigos;
    }
    public void EditarAmigo(Amigo amigoEscolhido, Amigo dadosEditados)
    {
        amigoEscolhido.Nome = dadosEditados.Nome;
        amigoEscolhido.Responsavel = dadosEditados.Responsavel;
        amigoEscolhido.Telefone = dadosEditados.Telefone;
    }
    public void ExcluirAmigo(Amigo amigoEscolhido)
    {
        for (int i = 0; i < Amigos.Length; i++)
        {
            if (Amigos[i] == null)
                continue;
            else if (Amigos[i].Id == amigoEscolhido.Id)
            {
                Amigos[i] = null!;
                break;
            }
        }
    }
    public Amigo SelecionarPorId(int idAmigoEscolhido)
    {
        foreach (Amigo a in Amigos)
        {
            if (a == null)
                continue;

            if (a.Id == idAmigoEscolhido)
                return a;
        }

        return null!;
    }
    public bool VerificarTelefoneNovoRegistro(Amigo novoAmigo)
    {
        for (int i = 0; i < Amigos.Length; i++)
        {
            if (Amigos[i] == null)
                continue;

            if (novoAmigo.Telefone == Amigos[i].Telefone && novoAmigo.Id == 0)
                return true;
        }

        return false;
    }
    public bool VerificarTelefoneEditarRegistro(Amigo amigoEscolhido, Amigo dadosEditados)
    {
        for (int i = 0; i < Amigos.Length; i++)
        {
            if (Amigos[i] == null)
                continue;

            if (dadosEditados.Telefone == Amigos[i].Telefone && amigoEscolhido.Id != Amigos[i].Id)
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
    public bool VerificarReservaAtiva(Amigo amigoEscolhido)
    {
        if (amigoEscolhido.Reserva == null)
            return false;

        if (amigoEscolhido.Reserva.Status == "Ativa")
            return true;
        else
            return false;
    }
}
