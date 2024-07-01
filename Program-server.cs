using System.Net;
using System.Net.Sockets;
using System.Text;
using System;
using System.IO.Ports;
using System.Threading;

class Server
{
    private static SerialPort _serialPort;
    private static bool _running;
    enum Options
    {
        JOIN,
        NEW,
        STOP
    }
    static void Main(string[] args)
    {
        try
        {
            _serialPort = new SerialPort("COM3", 9600, Parity.None, 8, StopBits.One);
            _serialPort.DataReceived += new SerialDataReceivedEventHandler(Handler);
            _serialPort.Open();

            _running = true;
            Console.WriteLine("Server is running...");

            while (_running)
            {
                Thread.Sleep(100); 
            }

            _serialPort.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
    }

    private static void Handler(object sender, SerialDataReceivedEventArgs e)
    {
        try
        {
            string message = _serialPort.ReadLine();
            Console.WriteLine("Received: " + message);

            string[] parts = message.Split('>');
            string command = parts[0];
            string id = parts[1];
            switch (command)
            {
                case "JOIN":
                    for (int i = 1; i <= 10; i++)
                    {
                        Console.WriteLine(i);
                    }
                    _serialPort.WriteLine($"{id}DONE");
                    break;
                case "NEW":
                    while (_running)
                    {
                        _serialPort.WriteLine($"CELLCOM>{id}");
                        Thread.Sleep(1000);
                    }
                    break;
                case "STOP":
                    _running = false;
                    _serialPort.WriteLine($"BYE>{id}");
                    break;
                default:
                    Console.WriteLine("Error");
                    throw new InvalidOperationException("Unexisting operation.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
    }
}
