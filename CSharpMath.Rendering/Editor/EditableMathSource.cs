namespace CSharpMath.Rendering {
  public readonly struct EditableMathSource : ISource {
    public EditableMathSource(Atoms.MathList mathList) => MathList = mathList;
    public string ErrorMessage => null;
    public bool IsValid => true;
    public Atoms.MathList MathList { get; }
  }
}