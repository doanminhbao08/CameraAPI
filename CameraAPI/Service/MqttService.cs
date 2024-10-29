using MQTTnet;
using MQTTnet.Client;

using System;
using System.Text;
using System.Threading.Tasks;

namespace YourNamespace.Services // Thay YourNamespace bằng tên không gian tên của bạn
{
	public class MqttService
	{
		private IMqttClient _mqttClient;

		public MqttService()
		{
			// Tạo một client MQTT mới
			var factory = new MqttFactory();
			_mqttClient = factory.CreateMqttClient();
		}

		// Phương thức để kết nối tới MQTT broker
		public async Task ConnectAsync(string brokerIp, int brokerPort, string topic)
		{
			var options = new MqttClientOptionsBuilder()
				.WithTcpServer(brokerIp, brokerPort) // Địa chỉ IP và cổng của broker
				.WithCleanSession() // Session sạch
				.Build();

			try
			{
				// Kết nối tới broker
				await _mqttClient.ConnectAsync(options);
				Console.WriteLine("Kết nối thành công tới MQTT broker.");

				// Đăng ký chủ đề để nhận dữ liệu
				await _mqttClient.SubscribeAsync(topic);
				Console.WriteLine($"Đã đăng ký chủ đề: {topic}");

				// Đăng ký sự kiện nhận tin nhắn

			}
			catch (Exception ex)
			{
				Console.WriteLine($"Lỗi khi kết nối tới MQTT broker: {ex.Message}");
				throw; // Ném lại ngoại lệ để xử lý ở nơi khác nếu cần
			}
		}

		// Phương thức để ngắt kết nối
		public async Task DisconnectAsync()
		{
			if (_mqttClient.IsConnected)
			{
				await _mqttClient.DisconnectAsync();
				Console.WriteLine("Ngắt kết nối thành công.");
			}
		}
	}
}
