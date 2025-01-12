using Entitas;

namespace Code.Gameplay.Input
{
   public class EmitInputSystem : IExecuteSystem
   {
      private readonly IInputService _inputService;
      private readonly IGroup<InputEntity> _inputs;

      public EmitInputSystem(InputContext input, IInputService inputService)
      {
         _inputService = inputService;
         _inputs = input.GetGroup(InputMatcher.AllOf(InputMatcher.Input));
      }

      public void Execute()
      {
         foreach (InputEntity input in _inputs)
         {
            if (_inputService.IsLeftMouseButtonPressed())
            {
               input.ReplaceClickInput(_inputService.GetClickPosition());
            }
            else
            {
               if (input.hasClickInput)
               {
                  input.RemoveClickInput();
               }
            }
         }
      }
   }
}