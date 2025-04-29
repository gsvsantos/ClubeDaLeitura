using ClubeDaLeitura.Compartilhado;
using ClubeDaLeitura.ModuloEmprestimo;
using ClubeDaLeitura.ModuloMulta;
using ClubeDaLeitura.ModuloReserva;

namespace ClubeDaLeitura.ModuloAmigo;

public class Amigo : EntidadeBase<Amigo>
{
    public string Nome { get; set; }
    public string Responsavel { get; set; }
    public string Telefone { get; set; }
    public List<Emprestimo> Emprestimos { get; set; } = new List<Emprestimo>();
    public List<Multa> Multas { get; set; } = new List<Multa>();
    public Reserva? Reserva { get; set; }

    public Amigo() { }
    public Amigo(string nome, string responsavel, string telefone)
    {
        Nome = nome;
        Responsavel = responsavel;
        Telefone = telefone;
    }
    public override string Validar()
    {
        string erros = "";

        if (string.IsNullOrWhiteSpace(Nome))
            erros += "\nO campo 'Nome' é obrigatório.\n";
        else
        {
            if (Nome.Length < 3 || !Nome.All(c => char.IsLetter(c) || c == ' '))
                erros += "O campo 'Nome' precisa conter ao menos 3 caracteres e apenas letras.\n";

            if (Nome.Length > 100)
                erros += "O campo 'Nome' não pode ter mais que 100 caracteres.\n";
        }

        if (string.IsNullOrWhiteSpace(Responsavel))
            erros += "O campo 'Responsável' é obrigatório.\n";
        else
        {
            if (Responsavel.Length < 3 || !Responsavel.All(c => char.IsLetter(c) || c == ' '))
                erros += "O campo 'Responsável' precisa conter ao menos 3 caracteres.";

            if (Responsavel.Length > 100)
                erros += "O campo 'Responsável' não pode ter mais que 100 caracteres.\n";
        }

        if (string.IsNullOrWhiteSpace(Telefone))
            erros += "O campo 'Telefone' é obrigatório.\n";
        else
        {
            bool formatoInvalido = false;

            if (Telefone.Length < 12 || Telefone.Length > 13)
                formatoInvalido = true;

            else if (Telefone.Length >= 3 && (!char.IsDigit(Telefone[0]) || !char.IsDigit(Telefone[1]) || Telefone[2] != ' '))
                erros += "O DDD deve ter dois números seguidos de um espaço (Exemplo: '51 ...').\n";

            else if (Telefone.Length == 12 && Telefone[7] != '-')
                formatoInvalido = true;

            else if (Telefone.Length == 13 && Telefone[8] != '-')
                formatoInvalido = true;

            else if (!Telefone.All(n => char.IsDigit(n) || n == ' ' || n == '-'))
                formatoInvalido = true;

            if (formatoInvalido)
                erros += "O campo 'Telefone' está errado! Use os formatos (00 0000-0000 ou 00 00000-0000).\n";
        }

        return erros;
    }
    public void ReceberEmprestimo(Emprestimo novoEmprestimo)
    {
        Emprestimos.Add(novoEmprestimo);
    }
    public List<Emprestimo> ObterEmprestimos()
    {
        return Emprestimos;
    }
    public bool VerificarEmprestimos()
    {
        int emprestimos = 0;

        if (Emprestimos == null)
            return false;

        foreach (Emprestimo e in Emprestimos)
        {
            if (e != null)
                emprestimos++;
        }

        if (emprestimos > 0)
            return true;
        else
            return false;
    }
    public bool VerificarEmprestimosAbertos()
    {
        if (Emprestimos.Any(e => e != null && e.Situacao != "Concluído"))
            return true;
        else
            return false;
    }
    public void ReceberMulta(Multa novaMulta)
    {
        Multas.Add(novaMulta);
    }
    public List<Multa> ObterMultas()
    {
        return Multas;
    }
    public bool VerificarMultas()
    {
        int multas = 0;

        if (Multas == null)
            return false;

        foreach (Multa m in Multas)
        {
            if (m != null && m.Status != "Quitada")
                multas++;
        }

        if (multas > 0)
            return true;
        else
            return false;
    }
    public void ReceberReserva(Reserva novaReserva)
    {
        if (Reserva == null)
            Reserva = novaReserva;
        else
            return;
    }
    public override void AtualizarRegistro(Amigo dadosEditados)
    {
        Nome = dadosEditados.Nome;
        Responsavel = dadosEditados.Responsavel;
        Telefone = dadosEditados.Telefone;
    }
}
