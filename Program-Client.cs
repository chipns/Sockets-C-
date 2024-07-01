﻿using System;
using System.IO.Ports;
using System.Threading;


public class Client
{
    private static SerialPort _serialPort;

    static void Main(string[] args)
    {
        try
        {

            _serialPort = new SerialPort("COM4", 9600, Parity.None, 8, StopBits.One);
            _serialPort.DataReceived += new SerialDataReceivedEventHandler(Handler);
            _serialPort.Open();

            Console.WriteLine("Client is running...");

            while (true)
            {
                System.Console.WriteLine("Please Chose an option (JOIN, NEW, STOP): ");
                string chosen = System.Console.ReadLine();
                _serialPort.WriteLine(chosen);

            }
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
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
    }

}

