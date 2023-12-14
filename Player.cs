using Godot;
using System;

namespace Pong
{
	public partial class Player : Area2D
	{
		public override void _Ready()
		{
			_screenSize = GetViewportRect().Size;
		}

		public override void _Process(double delta)
		{
			var velocity = Vector2.Zero; // The player's movement vector.

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

			var animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");

			if (velocity.Length() > 0)
			{
				velocity = velocity.Normalized() * Speed;
				animatedSprite2D.Play();
			}
			else
			{
				animatedSprite2D.Stop();
			}
		}

		[Export]
		private int Speed { get; set; } = 400;

		private Vector2 _screenSize;
	}
}
