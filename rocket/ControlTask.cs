using System;

namespace func_rocket;

public class ControlTask
{
    /*
    ������� ������� ��������� ������ �� ������ �� ���� (rocketTarget) � ������ ����������� �������� ������ (vectorDirection).
     
    ����� ��� ��������� ������ �������� ������ (vectorMoving), ������� �������� ������ ������� ����������� � ���������������� ������� �������� ������. 
     
    ����� ������� ��������� ���� ����� �������� �������� � �������� �� ������ �� ���� (angleMovingToTarget).
    ���� ���� ���� ������ ��� ����� 1e-2 (����� ������ � ����), �� ������� ���������� Turn.None, ��� ��������, ��� ������ ���������� �������� ��� ��������. 
     
    ���� ���� ������ 1e-2, �� ������� ����� ��� ������� (angleMovingToTarget /= 2) � ��������� ��� ����� �������:
    vectorRight � vectorLeft, ������� ���������� ��������� vectorMoving �� angleMovingToTarget � -angleMovingToTarget ��������������. 

    ����� ������� ��������� ���� ����� ������ �� ���� �������� � �������� �� ������ �� ���� (angleRight � angleLeft ��������������)
    � ���������� Turn.Left, ���� angleRight ������ angleLeft, � Turn.Right � ��������� ������. 

    ������� GetAngelBetweenVectors ��������� ���� ����� ����� ��������� � ������� ������� cos(����) = (a * b) / (|a| * |b|), ��� a � b - �������.
    */
    public static Turn ControlRocket(Rocket rocket, Vector target)
    {
        var rocketTarget = rocket.Location - target;
        var vectorDirection = new Vector(1, 0).Rotate(rocket.Direction);
        var vectorMoving = (vectorDirection + rocket.Velocity.Normalize());
        var angleMovingToTarget = GetAngelBetweenVectors(vectorMoving, rocketTarget);

        if (angleMovingToTarget <= 1e-2) { return Turn.None; }
        else angleMovingToTarget /= 2;

        var vectorRight = vectorMoving.Rotate(angleMovingToTarget);
        var vectorLeft = vectorMoving.Rotate(-angleMovingToTarget);
        var angleRight = GetAngelBetweenVectors(vectorRight, rocketTarget);
        var angleLeft = GetAngelBetweenVectors(vectorLeft, rocketTarget);

        if (angleRight < angleLeft) { return Turn.Left; }
        else { return Turn.Right; }
    }

    public static double GetAngelBetweenVectors(Vector vector1, Vector vector2)
    {
        return Math.Acos((vector1.X * vector2.X + vector1.Y * vector2.Y) / (vector1.Length * vector2.Length));
    }
}