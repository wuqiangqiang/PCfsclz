using System;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Xml;
using System.Configuration;

namespace FoodSafety.DI
{
    public static class ServiceFactory
    {
        /// <summary>
        /// 动态创建代理对象
        /// </summary>
        /// <typeparam name="T">服务契约</typeparam>
        /// <returns></returns>
        //避免多个服务引用
        public static T GetWcfService<T>() where T : class    
        {

            string serviceUrl = string.Format("{0}{1}.svc", 
                ConfigurationManager.AppSettings["ServiceUrl"],
                typeof(T).Name.Substring(1).Replace("Contract", "Service"));
            
            // 终结点对象 
            EndpointAddress remoteAddress = new EndpointAddress(serviceUrl);

            // 绑定
            BasicHttpBinding basicHttpBinding = new BasicHttpBinding
            {
                Security = { Mode = BasicHttpSecurityMode.None },
                MaxBufferPoolSize = int.MaxValue,
                MaxReceivedMessageSize = int.MaxValue,//获取或设置配置了此绑定的通道上可以接收的消息的最大大小。
                OpenTimeout = new TimeSpan(0, 1, 20),
                SendTimeout = new TimeSpan(0, 30, 0),
                ReceiveTimeout = new TimeSpan(0, 1, 20),
                CloseTimeout = new TimeSpan(0, 1, 20),
                ReaderQuotas = new XmlDictionaryReaderQuotas()
                {
                    MaxStringContentLength = int.MaxValue,
                    MaxArrayLength = int.MaxValue
                }
            };


            // 通道工厂
            ChannelFactory<T> channelFactory = new ChannelFactory<T>(basicHttpBinding, remoteAddress);

            foreach (OperationDescription operationDescription in channelFactory.Endpoint.Contract.Operations)
            {
                DataContractSerializerOperationBehavior operationBehavior =
                    operationDescription.Behaviors.Find<DataContractSerializerOperationBehavior>();

                if (operationBehavior != null)
                {
                    operationBehavior.MaxItemsInObjectGraph = int.MaxValue;
                }
            }


            return channelFactory.CreateChannel();
        }
    }
}
