using Godot;

namespace Pong
{
	public partial class Player : Area2D
	{
		public override void _Ready()
		{
			var animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
			_screenSize = GetViewportRect().Size;
			animatedSprite2D.Visible = false;
		}

		public override void _Process(double delta)
		{
			var velocity = Vector2.Zero; // The player's movement vector.
			var animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");

			if (Input.IsActionJustPressed("space"))
			{
				animatedSprite2D.Visible = !animatedSprite2D.Visible;
			}

			if (Input.IsActionPressed("move_right"))
			{
				velocity.X += 1;
			}

			if (Input.IsActionPressed("move_left"))
			{
				velocity.X -= 1;
			}

			if (Input.IsActionPressed("move_down"))
			{
				velocity.Y += 1;
			}

			if (Input.IsActionPressed("move_up"))
			{
				velocity.Y -= 1;
			}

			if (velocity.Length() > 0)
			{
				velocity = velocity.Normalized() * Speed;
				animatedSprite2D.Play();
			}
			else
			{
				animatedSprite2D.Stop();
			}

			Position += velocity * (float)delta;
			Position = new Vector2(
				x: Mathf.Clamp(Position.X, 0, _screenSize.X),
				y: Mathf.Clamp(Position.Y, 0, _screenSize.Y)
			);

			if (velocity.X != 0)
			{
				animatedSprite2D.Animation = "walk";
				animatedSprite2D.FlipV = false;
				// See the note below about boolean assignment.
				animatedSprite2D.FlipH = velocity.X < 0;
			}
			else if (velocity.Y != 0)
			{
				animatedSprite2D.Animation = "up";
			}
		}

		[Export]
		private int Speed { get; set; } = 400;

		private Vector2 _screenSize;
	}
}
