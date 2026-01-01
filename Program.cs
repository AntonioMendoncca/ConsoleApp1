using System;
using System.ComponentModel.Design;
using System.Reflection.PortableExecutable;

public class Program
{
    public static void Main(string[] args)
    {
        player Tester = new player();
        Tester.criar_player();
        
        luta combate = new luta();

        Inimigo monster = new Inimigo(100, 20,"grompe");
        monster.status();

        combate.batalhar(true, monster, Tester);
    }
}


public class luta
{
    public int energiaPlayer =100,energiaInimigo = 100;
    public bool rodada = true;

    public void rodadaInimigo(Inimigo inimigo, player player)
    {
        rodada = true;
        while (rodada == true)
        {
            Random rng = new Random();

            int acaoInimigo = rng.Next(1, 3);
            if (energiaInimigo >= 60)
            {
                if (acaoInimigo == 1)
                {
                    if (player.defesa == true)
                    {

                    }
                    else
                    {
                        Console.WriteLine($"O inimigo causa {inimigo.dano} em você!");
                        player.vida = player.vida - inimigo.dano;

                        energiaInimigo = energiaInimigo - 60;
                    }
                }
                else
                {
                    switch (rng.Next(1, 5))
                    {
                        case 1:
                            Console.WriteLine("TINK!!!!");
                            break;
                        case 2:
                            Console.WriteLine("PURRR!!");
                            break;
                        case 3:
                            Console.WriteLine("ROARAR!!");
                            
                            break;
                        case 4:
                            Console.WriteLine("SUQ!!");
                            
                            inimigo.defesa = true;
                            energiaInimigo = energiaInimigo - 60;
                            break;
                    }
                }
            }
            else
            {
                energiaInimigo = energiaInimigo + 30;
                rodada = false;
            }




        }
        
            
        
    }
    public void rodadaPlayer(Inimigo inimigo, player player)
    {
        Console.WriteLine(
            $"----------\n|vida:   {player.vida}|\n----------\n\n----------\n|Energia:{energiaPlayer}|\n----------\n\n" +
            "Acões:\n 1 - Socar[-60]\n 2 - Defender[-60]\n 3 - Passar a rodada[+30]"
        );
        rodada = true;
        while (rodada ==true)
        {
            string acao = Console.ReadLine();

            if (acao != "1" && acao != "2" && acao != "3")
            {
                Console.WriteLine("Tente novamente, acão invalida");
            }
            if (inimigo.vida == 0){
                Console.WriteLine($"Voce derrotou {inimigo.nome}!");
                break;
            }
            switch (acao)
            {
                case "1":
                    if (energiaPlayer >= 60 && inimigo.defesa==false) {

                        Console.WriteLine($"Voce soca o {inimigo.nome} causando {player.dano} de dano!");
                        inimigo.vida = inimigo.vida - player.dano; 
                    energiaPlayer = energiaPlayer - 60; }
                    else
                    {
                        Console.WriteLine($"Voce nao tem energia o suficiente!");
                    }

                        break;

                case "2":
                    if (energiaPlayer >= 60)
                    {

                        Console.WriteLine("Voce decide se defender do proximo ataque!");
                        player.defesa = true;
                        energiaPlayer = energiaPlayer - 60;
                    }
                    else
                    {
                        Console.WriteLine($"Voce nao tem energia o suficiente!");
                    }

                    break;

                case "3":
                    Console.WriteLine("Voce passa a rodada para o grompe!");
                    energiaPlayer = energiaPlayer + 30;
                    rodada = false;
                    break;
            }
        }
    }

    public void batalhar(bool OnOff, Inimigo inimigo, player player)
    {
        if (!OnOff) return;

        while (true)
        {
            Console.WriteLine("ATACAR [1] | FUGIR [2]");
            string acao = Console.ReadLine();

            if (acao == "1")
            {
                Console.WriteLine("Inicio da rodada!");
                rodadaPlayer(inimigo, player);
                rodadaInimigo(inimigo, player);
                if (player.vida==0 || inimigo.vida == 0)
                {
                    Console.WriteLine("O JOGO ACABOU...");
                    break;
                }
                
            }
            else if (acao == "2")
            {
                Console.WriteLine("Você fugiu!");
                break;
            }
            else
            {
                Console.WriteLine("Ação inválida, tente novamente");
            }
        }
    }
}

public class Inimigo
{
    public bool vivo;
    public int vida;
    public int dano;
    public string nome;
    public bool defesa = false;
    public Inimigo(int Vida, int Dano, string Nome)
    {
        nome = nome;
        vida = Vida;
        dano = Dano;
        vivo = true;
    }

    public void status()
    {
        Console.WriteLine($"\nMonster | {nome} |\nVida | {vida} |");
    }
}
public class player
{
    public string nome;
    public int vida;
    public int dano;
    public bool defesa = false;
    public void criar_player()
    {
        Console.WriteLine($"--------------------------------------------\nBEM VINDO A LUTA DA SELVA\n--------------------------------------------\nNOME DO LUTADOR:");
        nome = Console.ReadLine();
        Console.Write($"---------------------------------------\n|          Distribua 120 Pontos         |");
        int ponstosRestantes = 120;
        while (true) { 
            Console.Write($"\n|          Dano:");
            string imputDano = Console.ReadLine();
            if (!int.TryParse(imputDano, out dano))
            {
                Console.WriteLine("          [Valor invalido!]\n          [Digite um valor valido!]");
                continue;
            }
            if (dano >= ponstosRestantes || dano <= 0)
                {
                    Console.WriteLine("         [Valor invalido!]\n         [Voce deve digitar apenas numeros, entre 1 e 120!]");
                    continue;
                }
                else
                {
                    ponstosRestantes = ponstosRestantes - dano;
                    break;
                }
            }
        
        while (true) {
            Console.Write($"|          Vida:");
            String imputVida = Console.ReadLine();
            if (!int.TryParse(imputVida, out vida)) {
                Console.WriteLine("           [Valor invalido!]\n          [Digite um valor valido!]");
                continue;
            }
            if (vida <= 0 || vida > ponstosRestantes)
            {
                Console.WriteLine($"          [Valor invalido!]\n          [Voce deve digitar apenas numeros, entre 1 e {ponstosRestantes}!]");
                continue;
            }
            else
            {
                Console.Write($"----------------------------------------");
                break;
            }

        }
    }


}
