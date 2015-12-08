using System;
using System.Threading;

namespace SnakeGraph
{


	enum Colors:int
	{
		EField,
		EBody
	}


	class MainClass
	{
		private static int[,] cached_field;

		public static void Main (string[] args)
		{
			int w = 10; //Console.WindowWidth;
			int h = 12; //Console.WindowHeight;
			int[,] field = new int[h, w];
			field = BuildField (field);

			int[,] body = { { 1, 1 }, { 1, 2 }, { 1, 3 }, { 1, 4 } };
			DrowBody (body);
			ConsoleKeyInfo cki;
			bool playMode = true;
			do
			{
				
				cki = Console.ReadKey();
				ConsoleKey side;

				side = ConsoleKey.RightArrow;
				if (cki.Key == side)
				{
					do
					{
						Thread.Sleep(100);	
						ClearBody(body);
						body = BuildBody(field, body, side);
						DrowBody(body);
					} while(Console.KeyAvailable == playMode);
				}

				side = ConsoleKey.DownArrow;
				if (cki.Key == side)
				{
					do
					{
						Thread.Sleep(100);	
						ClearBody(body);
						body = BuildBody(field, body, side);
						DrowBody(body);
					} while(Console.KeyAvailable == playMode);
				}

				side = ConsoleKey.LeftArrow;
				if (cki.Key == side)
				{
					do
					{
						Thread.Sleep(100);	
						ClearBody(body);
						body = BuildBody(field, body, side);
						DrowBody(body);
					} while(Console.KeyAvailable == playMode);
				}

				side = ConsoleKey.UpArrow;
				if (cki.Key == side)
				{
					do
					{
						Thread.Sleep(100);	
						ClearBody(body);
						body = BuildBody(field, body, side);
						DrowBody(body);
					} while(Console.KeyAvailable == playMode);
				}

		
			}while (cki.Key != ConsoleKey.Escape);
		}

		static int[,] BuildField(int[,] _field)
		{

			var height = _field.GetLength (0);
			var weight = _field.GetLength (1);
			for (var i = 0; i < height; ++i) {
				for (var j = 0; j < weight; ++j) {
					_field [i, j] = (int)Colors.EField;
				}
			}
			Out (_field, null, Colors.EField);
			return _field;
		}

		static void DrowBody(int[,] _body)
		{
			//have to check correctness of body? e.g. continuity
			//have to check out of bound of field?
			Out(null, _body, Colors.EBody);
		}

		static void ClearBody(int[,] _body)
		{
			Out(null, _body, Colors.EField);
		}

		static int[,] BuildBody(int[,] _field, int[,] _body, ConsoleKey op)
		{
			var length = _body.GetLength(0);

			int[,] newbody = new int[length, 2];

			int[] head = { _body [0, 0], _body [0, 1] };
			int[] neck = { _body [1, 0], _body [1, 1] };

			ConsoleKey unavailableMove = ConsoleKey.LeftArrow;

			if (-1 == head [1] - neck [1])
				unavailableMove = ConsoleKey.RightArrow;
			else if (1 == head [1] - neck [1])
				unavailableMove = ConsoleKey.LeftArrow;
			else if (-1 == head [0] - neck [0])
				unavailableMove = ConsoleKey.DownArrow;
			else if (1 == head [0] - neck [0])
				unavailableMove = ConsoleKey.UpArrow;
			
			if (op == unavailableMove) return _body;

			if (op == ConsoleKey.LeftArrow && op != unavailableMove) {
				newbody [0, 0] = _body [0, 0];
				newbody [0, 1] = _body [0, 1] - 1;

			}
			else if (op == ConsoleKey.DownArrow && op != unavailableMove) {
				newbody [0, 0] = _body [0, 0] + 1;
				newbody [0, 1] = _body [0, 1];
			}
			else if (op == ConsoleKey.RightArrow && op != unavailableMove) {
				newbody [0, 0] = _body [0, 0] ;
				newbody [0, 1] = _body [0, 1] + 1;
			}
			else if (op == ConsoleKey.UpArrow && op != unavailableMove) {
				newbody [0, 0] = _body [0, 0] - 1;
				newbody [0, 1] = _body [0, 1];
			}


			for (var i = 1; i < length; ++i) {
				newbody [i, 0] = _body [i - 1, 0];
				newbody [i, 1] = _body [i - 1, 1];
			}

			return newbody;
		}





		static void Out(int[,] _field, int[,] _body, Colors _bodyColor)
		{
			//if we draw body only, we have to know field from the past
			if (_field == null)
				_field = cached_field;
			else
				cached_field = _field;

			var height = _field.GetLength (0);
			var weight = _field.GetLength (1);

			//if we draw field only, we dont need body at all
			if (_body != null)
			{
				var length = _body.GetLength (0);
				for (var i = 0; i < length; ++i) {
					var y = _body [i, 0];
					var x = _body [i, 1];

					y = ToGoodValue (0, height, y);
					x = ToGoodValue (0, weight, x);
					
					_field [y, x] = (int)_bodyColor;
				}
			}


			for (var i = 0; i < height; ++i) {
				for (var j = 0; j < weight; ++j) {
						Console.Write(_field[i,j]);
					}
					Console.WriteLine();
				}
			Console.WriteLine();
		}
		static int ToGoodValue(int _lowBorder, int _highBorder, int _cur)
		{
			while (false == _lowBorder <= _cur)
				_cur += _highBorder - _lowBorder;
			while (false == _highBorder > _cur)
				_cur -= _highBorder - _lowBorder;	
			return _cur;
		}

	}
}
