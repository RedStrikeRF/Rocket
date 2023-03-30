using System;

namespace func_rocket;

public class ControlTask
{
    /*
    —начала функци€ вычисл€ет вектор от ракеты до цели (rocketTarget) и вектор направлени€ движени€ ракеты (vectorDirection).
     
    «атем она вычисл€ет вектор движени€ ракеты (vectorMoving), который €вл€етс€ суммой вектора направлени€ и нормализованного вектора скорости ракеты. 
     
    ƒалее функци€ вычисл€ет угол между вектором движени€ и вектором от ракеты до цели (angleMovingToTarget).
    ≈сли этот угол меньше или равен 1e-2 (очень близко к нулю), то функци€ возвращает Turn.None, что означает, что ракета продолжает движение без поворота. 
     
    ≈сли угол больше 1e-2, то функци€ делит его пополам (angleMovingToTarget /= 2) и вычисл€ет два новых вектора:
    vectorRight и vectorLeft, которые получаютс€ поворотом vectorMoving на angleMovingToTarget и -angleMovingToTarget соответственно. 

    «атем функци€ вычисл€ет углы между каждым из этих векторов и вектором от ракеты до цели (angleRight и angleLeft соответственно)
    и возвращает Turn.Left, если angleRight меньше angleLeft, и Turn.Right в противном случае. 

    ‘ункци€ GetAngelBetweenVectors вычисл€ет угол между двум€ векторами с помощью формулы cos(угол) = (a * b) / (|a| * |b|), где a и b - векторы.
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