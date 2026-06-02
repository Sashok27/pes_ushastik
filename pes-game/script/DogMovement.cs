using Godot;

public partial class DogMovement : CharacterBody2D
{
	[Export] public float Speed = 200.0f;
	private AnimatedSprite2D _sprite;
	private Vector2 _originalScale;   // запоминаем оригинальный масштаб спрайта

	public override void _Ready()
	{
		_sprite = GetNode<AnimatedSprite2D>("DogAnimation");
		_originalScale = _sprite.Scale;   // сохраняем масштаб, который вы установили в редакторе
		_sprite.Play("stoim");
	}

	public override void _PhysicsProcess(double delta)
	{
		// Собираем вектор движения напрямую с клавиш
		Vector2 input = Vector2.Zero;
		if (Input.IsKeyPressed(Key.A) || Input.IsKeyPressed(Key.Left))
			input.X -= 1;
		if (Input.IsKeyPressed(Key.D) || Input.IsKeyPressed(Key.Right))
			input.X += 1;
		if (Input.IsKeyPressed(Key.W) || Input.IsKeyPressed(Key.Up))
			input.Y -= 1;
		if (Input.IsKeyPressed(Key.S) || Input.IsKeyPressed(Key.Down))
			input.Y += 1;

		// Нормализация для диагонали
		if (input.Length() > 1)
			input = input.Normalized();

		Velocity = input * Speed;
		MoveAndSlide();

		if (input != Vector2.Zero)
		{
			if (_sprite.Animation != "go_forward")
				_sprite.Play("go_forward");
			
			// Разворот: сохраняем оригинальный размер по X, меняем только знак
			if (input.X != 0)
			{
				float direction = Mathf.Sign(input.X);
				_sprite.Scale = new Vector2(direction * Mathf.Abs(_originalScale.X), _originalScale.Y);
			}
		}
		else
		{
			if (_sprite.Animation != "stoim")
				_sprite.Play("stoim");
		}
	}
}
