using System;
using Amazon.SQS;
using Amazon.SQS.Model;

namespace SQSReceiveMessage
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
                    // Read message from FIFO queue
                    var receiveMessageRequest = new ReceiveMessageRequest
                    {
                        QueueUrl = myQueueUrl
                    };

                    var receiveMessageResponse = amazonSqsClient.ReceiveMessage(receiveMessageRequest);
                    if (receiveMessageResponse.Messages != null)
                    {
                        var message = receiveMessageResponse.Messages[0];

                        if (!string.IsNullOrEmpty(message.Body))
                        {
                            Console.WriteLine("Read message: {0}", message.Body);
                        }


                        // Delete message from FIFO queue
                        var messageRecieptHandle = message.ReceiptHandle;
                        var deleteMessageRequest = new DeleteMessageRequest
                        {
                            QueueUrl = myQueueUrl,
                            ReceiptHandle = messageRecieptHandle
                        };
                        amazonSqsClient.DeleteMessage(deleteMessageRequest);
                        Console.WriteLine("Delete message: {0}", message.Body);
                    }
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