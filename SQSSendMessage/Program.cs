using System;
using Amazon.SQS;
using Amazon.SQS.Model;

namespace SQSSendMessage
{
    class Program
    {
        public static void Main(string[] args)
        {
            // SQSClient from AWSSDK
            var amazonSqsClient = new AmazonSQSClient();

            // FIFO quene URL
            string myQueueUrl = "https://sqs.us-west-2.amazonaws.com/781160412246/ecfe.fifo";

            try
            {
                // Send 3 messages to FIFO queue
                for (var i = 0; i < 3; i++)
                {
                    var message = "message" + i;
                    var sendMessageRequest = new SendMessageRequest
                    {
                        QueueUrl = myQueueUrl,
                        MessageBody = message,
                        MessageGroupId = "Senao"
                    };

                    amazonSqsClient.SendMessage(sendMessageRequest);
                    Console.WriteLine("Send message {0}", message);
                }
            }
            catch (AmazonSQSException ex)
            {
                Console.WriteLine("Caught Exception: " + ex.Message);
                Console.WriteLine("Response Status Code: " + ex.StatusCode);
                Console.WriteLine("Error Code: " + ex.ErrorCode);
                Console.WriteLine("Error Type: " + ex.ErrorType);
                Console.WriteLine("Request ID: " + ex.RequestId);
            }

            Console.WriteLine("Press Enter to continue...");
            Console.Read();
        }
    }
}