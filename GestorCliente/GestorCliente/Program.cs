using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Projeto2
{
    class Program
    {

        [System.Serializable]
        struct Client
        {
            public string nome;
            public string email;
            public string cpf;
        }

        static List<Client> clients = new List<Client>();

        enum Menu { Listagem = 1, Adicionar, Remover, Sair }

        static void Main(string[] args)
        {
            Carregar();
            bool escolheuSair = false;
            while (!escolheuSair)
            {
                Console.WriteLine("Sistema de clientes - Bem vindo!");
                Console.WriteLine("1-Listagem\n2-Adicionar\n3-Remover\n4-Sair");
                int intOp = int.Parse(Console.ReadLine());
                Menu opcao = (Menu)intOp;

                switch (opcao)
                {
                    case Menu.Adicionar:
                        Adicionar();
                        break;
                    case Menu.Listagem:
                        Listagem();
                        break;
                    case Menu.Remover:
                        Remover();
                        break;
                    case Menu.Sair:
                        escolheuSair = true;
                        break;
                }
                Console.Clear();
            }
        }


        static void Adicionar()
        {
            Client client = new Client();
            Console.WriteLine("Cadastro de cliente: ");
            Console.WriteLine("Nome do cliente: ");
            client.nome = Console.ReadLine();
            Console.WriteLine("Email do cliente: ");
            client.email = Console.ReadLine();
            Console.WriteLine("CPF do cliente:");
            client.cpf = Console.ReadLine();

            clients.Add(client);
            Salvar();
            Console.WriteLine("Cadastro concluído, aperte enter para sair.");
            Console.ReadLine();
        }

        static void Listagem()
        {

            if (clients.Count > 0) // SE tem pelo menos um cliente
            {
                Console.WriteLine("Lista de clientes: ");
                int i = 0;
                foreach (Client client in clients)
                {
                    Console.WriteLine($"ID: {i}");
                    Console.WriteLine($"Nome: {client.nome}");
                    Console.WriteLine($"E-mail: {client.email}");
                    Console.WriteLine($"CPF: {client.cpf}");
                    Console.WriteLine("====================================");
                    i++;

                }
            }
            else
            {
                Console.WriteLine("Nenhum cliente cadastrado!");
            }


            Console.WriteLine("Aperte enter para sair.");
            Console.ReadLine();
        }

        static void Remover()
        {
            Listagem();
            Console.WriteLine("Digite o ID do cliente que você quer remover:");
            int id = int.Parse(Console.ReadLine());
            if (id >= 0 && id < clients.Count)
            {

                clients.RemoveAt(id);
                Salvar();

            }
            else
            {
                Console.WriteLine("Id digitado é inválido, tente novamente!");
                Console.ReadLine();
            }
        }

        static void Salvar()
        {
            FileStream stream = new FileStream("clientes.dat", FileMode.OpenOrCreate);
            BinaryFormatter enconder = new BinaryFormatter();

            enconder.Serialize(stream, clients);

            stream.Close();
        }

        static void Carregar()
        {
            FileStream stream = new FileStream("clientes.dat", FileMode.OpenOrCreate);

            try
            {
                BinaryFormatter enconder = new BinaryFormatter();

                clients = (List<Client>)enconder.Deserialize(stream);

                if (clients == null)
                {
                    clients = new List<Client>();
                }
            }
            catch (Exception e)
            {
                clients = new List<Client>();
            }


            stream.Close();
        }

    }
}
