
public class Proceso
{
    public string Nombre { get; set; }
    public int Prioridad { get; set; }
    public int Tiempo { get; set; }

    public Proceso(string nombre, int tiempo, int prioridad)
    {
        this.Nombre = nombre;
        this.Tiempo = tiempo;
        this.Prioridad = prioridad;
    }

    public override string ToString()
    {
        return $"{Nombre} Tiempo: {Tiempo} Prioridad: {Prioridad}";
    }
}

public class MinHeap
{
    private List<Proceso> heap = new List<Proceso>();

    private int Parent(int i) { return (i - 1) / 2; }
    private int LeftChild(int i) { return (2 * i + 1); }
    private int RightChild(int i) { return (2 * i + 2); }

    public void Insert(Proceso proceso)
    {
        heap.Add(proceso);
        int current = heap.Count - 1;

        while (current > 0 && heap[Parent(current)].Tiempo > heap[current].Tiempo)
        {
            // Intercambia
            Proceso temp = heap[current];
            heap[current] = heap[Parent(current)];
            heap[Parent(current)] = temp;

            current = Parent(current);
        }
    }

    public void Heapify(int i)
    {
        int smallest = i;
        int left = LeftChild(i);
        int right = RightChild(i);

        if (left < heap.Count && heap[left].Tiempo < heap[smallest].Tiempo)
            smallest = left;

        if (right < heap.Count && heap[right].Tiempo < heap[smallest].Tiempo)
            smallest = right;

        if (smallest != i)
        {
            Proceso temp = heap[i];
            heap[i] = heap[smallest];
            heap[smallest] = temp;
            Heapify(smallest);
        }
    }

    public void BuildHeap(List<Proceso> procesos)
    {
        heap = procesos;

        for (int i = heap.Count / 2 - 1; i >= 0; i--)
        {
            Heapify(i);
        }
    }

    public void PrintHeap()
    {
        foreach (var proceso in heap)
        {
            Console.WriteLine(proceso);
        }
    }
}

public class MaxHeap
{
    private List<Proceso> heap = new List<Proceso>();

    private int Parent(int i) { return (i - 1) / 2; }
    private int LeftChild(int i) { return (2 * i + 1); }
    private int RightChild(int i) { return (2 * i + 2); }

    public void Insert(Proceso proceso)
    {
        heap.Add(proceso);
        int current = heap.Count - 1;

        while (current > 0 && heap[Parent(current)].Prioridad < heap[current].Prioridad)
        {
            // Intercambia
            Proceso temp = heap[current];
            heap[current] = heap[Parent(current)];
            heap[Parent(current)] = temp;

            current = Parent(current);
        }
    }

    public void Heapify(int i)
    {
        int largest = i;
        int left = LeftChild(i);
        int right = RightChild(i);

        if (left < heap.Count && heap[left].Prioridad > heap[largest].Prioridad)
            largest = left;

        if (right < heap.Count && heap[right].Prioridad > heap[largest].Prioridad)
            largest = right;

        if (largest != i)
        {
            Proceso temp = heap[i];
            heap[i] = heap[largest];
            heap[largest] = temp;
            Heapify(largest);
        }
    }

    public void BuildHeap(List<Proceso> procesos)
    {
        heap = procesos;

        for (int i = heap.Count / 2 - 1; i >= 0; i--)
        {
            Heapify(i);
        }
    }

    public void PrintHeap()
    {
        foreach (var proceso in heap)
        {
            Console.WriteLine(proceso);
        }
    }
}

public class Program
{
    public static List<Proceso> ConvertirCsvAProcesos(string rutaArchivo)
    {
        var procesos = new List<Proceso>();
        var lineas = File.ReadAllLines(rutaArchivo);
        foreach (var linea in lineas)
        {
            var partes = linea.Split(';');
            if (partes.Length == 3)
            {
                var nombre = partes[0];
                var tiempo = int.Parse(partes[1]);
                var prioridad = int.Parse(partes[2]);
                var proceso = new Proceso(nombre, tiempo, prioridad);
                procesos.Add(proceso);
            }
        }
        return procesos;
    }

    public static void Main()
    {
        string rutaArchivo = "C:\\Users\\User\\source\\repos\\proces1archivoSjs\\proces1archivoSjs\\dataset.csv";
        var procesos = ConvertirCsvAProcesos(rutaArchivo);
        bool continuar = true;

        do
        {
            Console.Clear();
            Console.WriteLine("¿Cómo querés ver los procesos?");
            Console.WriteLine("1 - Orden normal (imprime los datos tal cual están en el CSV)");
            Console.WriteLine("2 - Min heap");
            Console.WriteLine("3 - Max heap");
            Console.WriteLine("4 - Salir");

            string opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1":
                    foreach (var proceso in procesos)
                    {
                        Console.WriteLine(proceso);
                    }
                    break;
                case "2":
                    MinHeap minHeap = new MinHeap();
                    minHeap.BuildHeap(procesos);
                    minHeap.PrintHeap();
                    break;
                case "3":
                    MaxHeap maxHeap = new MaxHeap();
                    maxHeap.BuildHeap(procesos);
                    maxHeap.PrintHeap();
                    break;
                case "4":
                    continuar = false;
                    break;
                default:
                    Console.WriteLine("Opción no válida.");
                    break;
            }

            if (continuar)
            {
                Console.WriteLine("Presiona Enter para continuar...");
                Console.ReadLine();
            }

        } while (continuar);
    }


}
