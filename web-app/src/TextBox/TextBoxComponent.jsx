import React, { Component } from 'react';

class TextBoxComponent extends Component {

  constructor(props){
    super(props);

    this.state = {
      value: this.props.value,
      timeOut: undefined
    };
    this.onChangeHandler = this.onChangeHandler.bind(this);
    this.onBlurHandler = this.onBlurHandler.bind(this);
  }

  onChangeHandler(evt){
    clearTimeout(this.state.timeOut);
    const nextValue = evt.target.value;
    this.setState({
      value: nextValue,
      timeOut: setTimeout(() => {
        this.props.onChange(nextValue);
      }, 250)})
  }

  onBlurHandler(evt){
    clearTimeout(this.state.timeOut);    
    this.setState({value: evt.target.value});
    this.props.onChange( this.state.value );
  }

  componentWillReceiveProps(next){
    this.setState({
      value: next.value
    })
  }
  
  render(){
    return (
      <input type="text" value={this.state.value} onChange={this.onChangeHandler.bind(this)} onBlur={this.onBlurHandler.bind(this)}/>
    )
  }
}

export default TextBoxComponent;