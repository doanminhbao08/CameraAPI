using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using YourNamespace.Services; // Thay YourNamespace bằng tên không gian tên của bạn

namespace YourNamespace.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class CameraController : ControllerBase
	{
		private readonly MqttService _mqttService;

		public CameraController(MqttService mqttService)
		{
			_mqttService = mqttService; // Inject MqttService
		}

		[HttpGet("connect")]
		public async Task<IActionResult> ConnectToMqtt()
		{
			string brokerIp = "10.10.22.14"; // Địa chỉ IP của MQTT broker
			int brokerPort = 1883; // Cổng MQTT
			string topic = "/topic/detected/$camera_id"; // Chủ đề mà camera sẽ gửi dữ liệu

			try
			{
				await _mqttService.ConnectAsync(brokerIp, brokerPort, topic);
				return Ok("Đã kết nối tới MQTT broker");
			}
			catch (System.Net.Sockets.SocketException ex)
			{
				return StatusCode(500, $"Không thể kết nối tới MQTT broker. Lỗi: {ex.Message}");
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Lỗi không xác định: {ex.Message}");
			}
		}

		// Thêm các phương thức khác nếu cần
	}
}


// telnet 10.10.22.14 1883
