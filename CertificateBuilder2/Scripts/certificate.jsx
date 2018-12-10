
/******** App ********/
class CertificateBuilder extends React.Component {
    constructor(props) {
    super(props);
    this.state = {
            Id: this.props.initialData.Id,
            Coursekey: this.props.initialData.Coursekey,
            UserGroupkey: this.props.initialData.UserGroupkey,
            Page_name: this.props.initialData.Page_name,
            Orientation: this.props.initialData.Orientation,
            Image_src: this.props.initialData.Image_src,
  
            Greet: this.props.initialData.Greet,
            Fullname: this.props.initialData.Fullname,
            Course: this.props.initialData.Course,
            Completed: this.props.initialData.Completed,
            Duration: this.props.initialData.Duration,
            Statement1: this.props.initialData.Statement1,
            Statement2: this.props.initialData.Statement2,
            Statement3: this.props.initialData.Statement3,
            Statement4: this.props.initialData.Statement4
        };

        this.handleChange = this.handleChange.bind(this);
        this.handleDrag = this.handleDrag.bind(this);
        this.handleColorChange = this.handleColorChange.bind(this);
        this.handleSaveContinue = this.handleSaveContinue.bind(this);
        this.handleSaveExit = this.handleSaveExit.bind(this);
        this.handleExit = this.handleExit.bind(this);
    }
  componentDidMount() {

   }

    handleChange (name, value, key) {
        var stateToUpdate = this.state[key];
        stateToUpdate[name] = value;

        this.setState({
          [key]: stateToUpdate
        });
    }

    handleDrag (x, y, key) {
        var stateToUpdate = this.state[key];
        stateToUpdate.X_Coordinate = x;
        stateToUpdate.Y_Coordinate = y;
        
        this.setState({
             [key]: stateToUpdate
        })
    }

    handleColorChange(r, g, b, key) {
        var stateToUpdate = this.state[key];
        stateToUpdate.Color_Red = r;
        stateToUpdate.Color_Green = g;
        stateToUpdate.Color_Blue = b;

        this.setState({
            [key]: stateToUpdate
        })
    }

    handleSaveContinue(e) {
        e.preventDefault();
        
        var Certificate = this.state;

        var xhr = new XMLHttpRequest();
        xhr.open('POST', "/saveandcontinue", true);
        xhr.setRequestHeader('Content-Type', 'application/json; charset=UTF-8');
         xhr.onload = function() {
        var data = JSON.parse(xhr.responseText);
         this.setState({
            Id: data
         });
        }.bind(this);
        xhr.send(JSON.stringify(Certificate));

    }

    handleSaveExit(e) {
         e.preventDefault();
        
        var Certificate = this.state;

        var xhr = new XMLHttpRequest();
        xhr.open('POST', "/saveandexit", true);
        xhr.setRequestHeader('Content-Type', 'application/json; charset=UTF-8');
        
        xhr.onload = function (e) {
            if (xhr.readyState === 4 && xhr.status === 200) {
                window.location = '/index';
            }
            else {
                console.log(xhr.responseText);
                console.error(xhr.statusText);
            }
        }

        xhr.send(JSON.stringify(Certificate));

        //window.location = '/Certificate';
    }

    handleExit(e) {
        window.location = '/index';
    }

    render() {

        return (
          <div>
             <div className="col-sm-4">
                 <div className="container">
                     <DraggableBox element={this.state.Greet} onDrag={this.handleDrag} />
                     <DraggableBox element={this.state.Fullname}  onDrag={this.handleDrag} />
                     <DraggableBox element={this.state.Course} onDrag={this.handleDrag} />
                     <DraggableBox element={this.state.Completed}  onDrag={this.handleDrag} />
                     <DraggableBox element={this.state.Duration}  onDrag={this.handleDrag} />
                     {this.state.Statement1.Disabled ? null : <DraggableBox element={this.state.Statement1}  onDrag={this.handleDrag} />}
                     {this.state.Statement2.Disabled ? null : <DraggableBox element={this.state.Statement2}  onDrag={this.handleDrag} />}
                     {this.state.Statement3.Disabled ? null : <DraggableBox element={this.state.Statement3}  onDrag={this.handleDrag} />}
                     {this.state.Statement4.Disabled ? null : <DraggableBox element={this.state.Statement4}  onDrag={this.handleDrag} />}
                </div>
                <div>
                <br/>
                <label><strong>Page Name: </strong>{this.state.Page_name}</label>
                </div>
                <div>
                <label><strong>Orientation: </strong>{this.state.Orientation}</label>
                </div>
                <div>
                <label><strong>Image Source: </strong>{this.state.Image_src}</label>
                </div>
                <div>
                <label><strong>Certificate Id: </strong>{this.state.Id}</label>
                </div>
                <div>
                <label><strong>Coursekey: </strong>{this.state.Coursekey}</label>
                </div>
                <div>
                <label><strong>UserGroupkey: </strong>{this.state.UserGroupkey}</label>
                </div>
                <div>
                <button className="btn btn-default buttonpad" type="submit" onClick={this.handleSaveContinue}>Save and Continue</button>
                <button className="btn btn-default buttonpad" type="submit" onClick={this.handleSaveExit}>Save and Exit</button>
                <button className="btn btn-default buttonpad" onClick={this.handleExit}>Exit</button>
                </div>
            </div>

        <div className="col-sm-8">
        
            <ControlPanel element={this.state.Greet}
            onChange={this.handleChange} onColorChange={this.handleColorChange} />
            <ControlPanel element={this.state.Fullname}
            onChange={this.handleChange} onColorChange={this.handleColorChange} />
            <ControlPanel element={this.state.Course}
            onChange={this.handleChange} onColorChange={this.handleColorChange} />
            <ControlPanel element={this.state.Completed}
            onChange={this.handleChange} onColorChange={this.handleColorChange} />
            <ControlPanel element={this.state.Duration}
            onChange={this.handleChange} onColorChange={this.handleColorChange} />
            <ControlPanel element={this.state.Statement1}
            onChange={this.handleChange} onColorChange={this.handleColorChange} />
            <ControlPanel element={this.state.Statement2}
            onChange={this.handleChange} onColorChange={this.handleColorChange} />
            <ControlPanel element={this.state.Statement3}
            onChange={this.handleChange} onColorChange={this.handleColorChange} />
            <ControlPanel element={this.state.Statement4}
            onChange={this.handleChange} onColorChange={this.handleColorChange} />
        
        </div>

    </div>
   );
 }

};

/******** Panels ********/
class ControlPanel extends React.Component {
    constructor(props) {
    super(props);
    this.state = {
            /*align buttons and most recently clicked*/
            alignButtonLeft: "btn btn-default",
            alignButtonCenter: "btn btn-primary",
            alignButtonRight: "btn btn-default"

        };
        this.handleChange = this.handleChange.bind(this);
        this.handleColorChange = this.handleColorChange.bind(this);
        this.handleAlignClick = this.handleAlignClick.bind(this);
        this.handleDisableChecked = this.handleDisableChecked.bind(this);
        this.getPlaceHolderText = this.getPlaceHolderText.bind(this);
    }

    handleChange (e) {
        this.props.onChange(e.target.name, e.target.value, this.props.element.Name);
    }

    handleColorChange (e) {

        var color = e.target.value;
        var r = '';
        var g = '';
        var b = '';
        switch(color) {
            case '000':
                r = 0; g = 0; b = 0;
                break;
            case '25500':
                r = 255; g = 0; b = 0;
                break;
            case '2551280':
                r = 255; g = 128; b = 0;
                break;
            case '2552550':
                r = 255; g = 255; b = 0;
                break;
            case '02550':
                r = 0; g = 255; b = 0;
                break;
            case '00255':
                r = 0; g = 0; b = 255;
                break;
            case '2550255':
                r = 255; g = 0; b = 255;
                break;
            case '255255255':
                r = 255; g = 255; b = 255;
                break;
            default:
                break;
        }
        
        this.props.onColorChange(r,g,b, this.props.element.Name);
    }

    handleAlignClick (e) {
        e.preventDefault();
        this.props.onChange(e.currentTarget.name, e.currentTarget.value, this.props.element.Name)

        let recentlyClicked = 'alignButton'+e.currentTarget.value;

        this.setState({
            alignButtonLeft: "btn btn-default",
            alignButtonCenter: "btn btn-default",
            alignButtonRight: "btn btn-default",

          [recentlyClicked]: "btn btn-primary"
        });
    }

    handleDisableChecked (e) {
        this.props.onChange(e.target.name, e.target.checked, this.props.element.Name);
    }

    getPlaceHolderText(n) {
        switch(n) {
                case "Fullname": 
                    return "{Learner Name}"   
                case "Course": 
                    return "{Course Title}"  
                case "Completed": 
                    return "{Completed Date}"  
                case "Duration": 
                    return "{Course Duration}"
                default:
                    return "Enter text"
            };
    }

     render () {
        const element = this.props.element;
        var placeHolderText = this.getPlaceHolderText(this.props.element.Name);

        return (
          <div className="control-panel">
              {/*Input Field*/}
        <div className="filterDiv">
            <label className="formLabels">{element.Title}</label>
            <br />
          <input type="text"
        name="Text" className="inputfield textbox"
        placeholder={placeHolderText} value={element.Text}
        onChange={this.handleChange}
        disabled={(element.Disabled || element.Text_is_static )} />
</div>
     {/*Font Size*/}
          <div className="filterDiv">
            <label className="formLabels">
                Font Size
              <select name="Font_Size" value={element.Font_Size} onChange={this.handleChange} className="fontsizeDDL filterDDL form-control" disabled={element.Disabled}>
                <option value="12">12</option>
                <option value="16">16</option>
                <option value="20">20</option>
                <option value="24">24</option>
                <option value="28">28</option>
              </select>
            </label>
          </div>
{/*Font Type*/}
<div className="filterDiv">
  <label className="formLabels">
      Font Type
    <select name="Font_Type" value={element.Font_Type} onChange={this.handleChange} className="fonttypeDDL filterDDL form-control" disabled={element.Disabled}>
      <option value="Helvetica">Helvetica</option>
      <option value="Times">Times</option>
      <option value="Arial">Arial</option>
      <option value="Courier">Courier</option>
      <option value="Impact">Impact</option>
    </select>
  </label>
</div>
{/*Font Style*/}
<div className="filterDiv">
  <label className="formLabels">
      Font Style
    <select name="Font_Style" value={element.Font_Style} onChange={this.handleChange} className="fontstyleDDL filterDDL form-control" disabled={element.Disabled}>
      <option value="Normal">Normal</option>
      <option value="Bold">Bold</option>
      <option value="Italic">Italic</option>
      <option value="Underline">Underlined</option>
    </select>
  </label>
</div>
{/*Color Picker*/}
<div className="filterDiv">
  <label className="formLabels">Font Color</label> <br />
    <select name="Font_Style" value={[element.Color_Red] + [element.Color_Green] + [element.Color_Blue]} onChange={this.handleColorChange} className="fontstyleDDL filterDDL form-control" >
            <option className="black" value="000">Black</option>            
            <option className="red" value="25500">Red</option>
            <option className="orange" value="2551280">Orange</option>
            <option className="yellow" value="2552550">Yellow</option>
            <option className="green" value="02550">Green</option>
            <option className="blue" value="00255">Blue</option>
            <option className="magenta" value="2550255">Magenta</option>
            <option className="white" value="255255255">White</option>
        </select>
</div>
{/*Text Align*/}
<div className="filterDiv">
  <label className="formLabels">Alignment</label> <br />
  <div className="btn-group textAlignment">
      {/* These buttons should have glyphicons */}
    <button name="Text_Align" className={this.state.alignButtonLeft} onClick={this.handleAlignClick} value="Left" disabled={element.Disabled}><i className="glyphicon glyphicon-align-left"></i></button>
    <button name="Text_Align" className={this.state.alignButtonCenter} onClick={this.handleAlignClick} value="Center" disabled={element.Disabled}><i className="glyphicon glyphicon-align-center"></i></button>
    <button name="Text_Align" className={this.state.alignButtonRight} onClick={this.handleAlignClick} value="Right" disabled={element.Disabled}><i className="glyphicon glyphicon-align-right"></i></button>
  </div>
</div>
{/*Disable Panel*/}
<div className={'filterDiv '+((element.Text_is_static || element.Name === 'Greet') ? 'hideDisable' : '')}>
  <label className="formLabels">Disable</label> <br />
  <input name="Disabled" type="checkbox" className="disableControlPanel" defaultChecked={element.Disabled} onChange={this.handleDisableChecked} />
</div>
          </div>
      );
    }
};


/******** Certificate Elements ********/
class DraggableBox extends React.Component {

    constructor(props) {
    super(props);
    this.state = {
              pos: {
                 x: this.props.element.X_Coordinate,
                 y: this.props.element.Y_Coordinate
             },
         dragging: false,
         rel: null // position relative to the cursor
       };

        this.onMouseDown = this.onMouseDown.bind(this);
        this.onMouseUp = this.onMouseUp.bind(this);
        this.onMouseMove = this.onMouseMove.bind(this);
     }

  componentDidUpdate (props, state) {
    if (this.state.dragging && !state.dragging) {
      document.addEventListener('mousemove', this.onMouseMove)
      document.addEventListener('mouseup', this.onMouseUp)
    } else if (!this.state.dragging && state.dragging) {
      document.removeEventListener('mousemove', this.onMouseMove)
      document.removeEventListener('mouseup', this.onMouseUp)
    }

  }

// This will set the location based on the props on first render
    componentWillReceiveProps (props) {
         this.setState({
            pos: {
              x: props.element.X_Coordinate, 
              y: props.element.Y_Coordinate 
            }    
        });
    }

    onMouseDown (e) {
    // only left mouse button
       if (e.button !== 0) return // only left mouse button
        // var pos = ReactDOM.findDOMNode(this).getBoundingClientRect();
        //console.log(ReactDOM.findDOMNode(this.refs[this.props.element.Name]));
       this.setState({
         dragging: true,
         rel: {
            x: e.pageX - this.state.pos.x,
            y: e.pageY - this.state.pos.y
       }
       });
       e.stopPropagation();
       e.preventDefault();
     }

      onMouseUp (e) {

        this.props.onDrag(this.state.pos.x, this.state.pos.y, this.props.element.Name)

        this.setState({dragging: false});
        e.stopPropagation();
        e.preventDefault();
      }

    onMouseMove (e) {
        if (!this.state.dragging) return
        // Need to validate based on the size of the provided image (provided from props?)
    
        var x = e.pageX - this.state.rel.x;
        var y = e.pageY - this.state.rel.y;

       if ((x < 550) && (0 < x) && (y < 645) && (0 < y)) {

          this.setState({
            pos: {
              x: x, 
              y: y 
            }    
        });
       }

       e.stopPropagation()
        e.preventDefault()
      }

    displayText(el) {
       switch(el.Name) {
                case "Fullname": 
                    return "{Learner Name}"   
                case "Course": 
                    return "{Course Title}"  
                case "Completed": 
                    return "{Completed Date}"  
                case "Duration": 
                    return "{Course Duration}"
                default:
                    return el.Text
            };
    }

    render () {
        const element = this.props.element;
        var displayText = this.displayText(this.props.element);  

        return (
       
        <div className="draggable" onMouseDown={this.onMouseDown} onMouseUp={this.onMouseUp} onMouseMove={this.onMouseMove} style={{left: this.state.pos.x + 'px', top: this.state.pos.y + 'px'}}> 
        <label ref={element.Name} id={element.Name} 
        className={'draggable-text '+'fontFamily'+[element.Font_Type]+' fontStyle'+[element.Font_Style]+' align'+[element.Text_Align]}
        style={{fontSize:element.Font_Size+'px', color: `rgb(`+[element.Color_Red]+`,`+ [element.Color_Green]+`,`+[element.Color_Blue]+`)`}}>
            {displayText}
        </label> 
          </div>
        );
}
};

/*
ReactDOM.render(<CertificateBuilder url="/getcertificate" />, document.getElementById("root"));
*/