using System;
using UnityEngine.EventSystems;

namespace Util
{
    public class DieState
    {
        public DieEffect FaceCentral { get; private set; }
        public DieEffect FaceUp { get; private set;}
        public DieEffect FaceLeft { get; private set;}
        public DieEffect FaceRight { get; private set;}
        public DieEffect FaceDown { get; private set;}
        public DieEffect FaceDoubleDown { get; private set;}
        
        public DieState(DieConfiguration dieConfiguration)
        {
            FaceCentral = dieConfiguration.FaceCentral;
            FaceUp = dieConfiguration.FaceUp;
            FaceLeft = dieConfiguration.FaceLeft;
            FaceRight = dieConfiguration.FaceRight;
            FaceDown = dieConfiguration.FaceDown;
            FaceDoubleDown = dieConfiguration.FaceDoubleDown;
        }

        public void ChangeState(MoveDirection moveDirection)
        {
            switch (moveDirection)
            {
                case MoveDirection.Left:
                    MoveLeft();
                    break;
                case MoveDirection.Up:
                    MoveUp();
                    break;
                case MoveDirection.Right:
                    MoveRight();
                    break;
                case MoveDirection.Down:
                    MoveDown();
                    break;
            }
        }
        private void MoveLeft()
        {
            var newRight = FaceDoubleDown;
            FaceDoubleDown = FaceLeft;
            FaceLeft = FaceCentral;
            FaceCentral = FaceRight;
            FaceRight = newRight;
        }
        private void MoveUp()
        {
            var newDoubleDown = FaceUp;
            FaceUp = FaceCentral;
            FaceCentral = FaceDown;
            FaceDown = FaceDoubleDown;
            FaceDoubleDown = newDoubleDown;
        }
        private void MoveRight()
        {
            var newLeft = FaceDoubleDown;
            FaceDoubleDown = FaceRight;
            FaceRight = FaceCentral;
            FaceCentral = FaceLeft;
            FaceLeft = newLeft;
        }
        private void MoveDown()
        {
            var newUp = FaceDoubleDown;
            FaceDoubleDown = FaceDown;
            FaceDown = FaceCentral;
            FaceCentral = FaceUp;
            FaceUp = newUp;
        }
    }
}