using System;
using System.Collections;

namespace Dijkstra
{
    class Program
    {
        static void Inicializar(Grafo grafo, Vertice v)
        {
            foreach (KeyValuePair<string, Vertice> x in grafo.ObterVertices())
            {
                x.Value.Distancia = int.MaxValue;
            }

            v.Distancia = 0;
        }

        static Vertice ExtrairMinimo(Dictionary<string, Vertice> x)
        {
            var chave = x.Keys.First();
            Vertice min = x[chave];

            foreach (KeyValuePair<string, Vertice> vrt in x)
            {
                min = vrt.Value.Distancia < min.Distancia ? vrt.Value : min;
            }

            x.Remove(min.Id); ;
            return min;
        }

        static void RelaxarArestas(Vertice x, Vertice y)
        {
            int distancia = x.Distancia + x.ObterCusto(y);
            if (y.Distancia > distancia)
            {
                y.Distancia = distancia;
                y.Anterior = x;
            }
        }

        static void AdicionarAoDicionario(Vertice v, Dictionary<string, Vertice> dicionario)
        {
            if (!dicionario.ContainsKey(v.Id))
            {
                dicionario.Add(v.Id, v);
            }
        }

        static void CalcularCaminho(Vertice destino, ArrayList caminho) 
        {
            if (caminho.Count == 0) 
            {
                caminho.Add(destino);
            }

            while (destino.Anterior != null) 
            {
                caminho.Add(destino.Anterior);
                destino = destino.Anterior;
            }
        }

        static void Dijkstra(Grafo grafo, Vertice origem)
        {
            Dictionary<string, Vertice> x = new();
            Inicializar(grafo, origem);

            x = grafo.ObterVertices();

            Dictionary<string, Vertice> y = new();
            while (x.Count > 0)
            {
                //extrai e retorna o vértice com as arestas incidentes com menor custo até que o dicionário esteja vazio, visando iterar todos os registros
                Vertice vert = ExtrairMinimo(x);
                vert.Visitou = true;

                foreach (KeyValuePair<Vertice, int> temp in vert.ObterAdjacentes())
                {
                    if (!temp.Key.Visitou)
                    {
                        //realiza uma estimativa pessimista e tenta melhorar a estimativa, buscando arestas de custo menor
                        RelaxarArestas(vert, temp.Key);
                    }
                }

                //cria um dicionário contendo os vértices atualizados com caminho mínimo do vértice de origem
                AdicionarAoDicionario(vert, y);
            }

            //Atualiza o grafo com os vértices contendo o caminho mínimo do vértice de origem
            grafo.DefinirVertices(y);
        }

        static void ImprimirCaminho(ArrayList caminho, Vertice origem, Vertice destino) 
        {
            Console.Write("O melhor caminho de '{0}' até '{1}' é: ", origem.Id, destino.Id);
            for (int i = caminho.Count-1; i >= 0; i--) 
            {
                Vertice vert = (Vertice)caminho[i];
                string complemento = i > 0 ? " > " : ", ";
                
                Console.Write(vert.Id + complemento);
            }
            Console.Write("com custo {0}.", destino.Distancia);
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Algoritmo de Dijkstra");
            Console.Write("O grafo é direcionado? (S/N): ");

            bool ehDirecionado = "S".Equals(Console.ReadLine().ToUpper());

            Grafo grafo = new Grafo(ehDirecionado);

            Console.Write("Insira a quantidade de vertices: ");
            int qtd = int.Parse(Console.ReadLine());

            for (int i = 1; i <= qtd; i++)
            {
                Console.Write("Insira o {0}º vértice: ", i);
                grafo.AdicionarVertices(Console.ReadLine());
            }

            for (int x = 0; x < grafo.ObterVertices().Count; x++)
            {
                int start = ehDirecionado ? 0 : x;

                for (int y = start; y < grafo.ObterVertices().Count; y++)
                {
                    var orig = grafo.ObterVertices().ElementAt(x).Key;
                    var dest = grafo.ObterVertices().ElementAt(y).Key;

                    Console.Write("Informe a distância de '{0}' para '{1}': ", orig, dest);
                    int custo = int.Parse(Console.ReadLine());
                    if (custo < 0) 
                    {
                        Console.WriteLine("Não é permitido inserir peso inferior a zero nas arestas.");
                        return;
                    }

                    grafo.AdicionarAresta(orig, dest, custo);
                }
            }

            Console.Write("Informe o vértice de origem: ");
            Vertice origem = grafo.ObterVertice(Console.ReadLine());

            Console.Write("Informe o vértice destino: ");
            Vertice destino = grafo.ObterVertice(Console.ReadLine());

            if (origem.Equals(destino)) 
            {
                Console.WriteLine("Vértice de origem é o mesmo vértice de destino.");
                return;
            }

            ArrayList caminho = new();
            Dijkstra(grafo, origem);

            //visita os vértices adjacentes ao vértice de destino até esgotar as possibilidades, traçando o caminho inverso até o vertice de origem
            CalcularCaminho(destino, caminho);

            if (caminho.Count == 0 || destino.Anterior == null)
            {
                Console.WriteLine("Não existe caminho de '{0}' até '{1}'.", origem.Id, destino.Id);
            }
            else
            {
                ImprimirCaminho(caminho, origem, destino);
            }
        }
    }
}
