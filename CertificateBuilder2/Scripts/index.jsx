
/******** App ********/
class IndexView extends React.Component {

constructor(props) {
    super(props)
    this.state = {
            ListItem: [] 
        };
        
        this.handleDelete = this.handleDelete.bind(this);
        this.handleEdit = this.handleEdit.bind(this);
}

    handleNewCert(e) {
        window.location = '/certificate/0';
    }

componentDidMount() {
    var xhr = new XMLHttpRequest();
    xhr.open('get',this.props.url, true);
    xhr.onload = function() {
        var data = JSON.parse(xhr.responseText);
         this.setState({
            ListItem: data,
        });
    }.bind(this);
    xhr.send(); 
    //this.getData();
   }

   handleDelete(CertId) {
       
        var xhr = new XMLHttpRequest();
        xhr.open('POST', "/deletecertificate/" + CertId, true);
        xhr.onload = function() {
        var data = JSON.parse(xhr.responseText);
         this.setState({
            ListItem: data,
        });
    }.bind(this);
    xhr.send();
    }

   handleEdit(CertId) {
       
       /* var xhr = new XMLHttpRequest();
        xhr.open('POST', "/certificate/" + CertId, true);
        xhr.send(); */
        window.location = '/certificate/'+CertId;
    }



 render() {

        return (
          <div>
           <div className="col-sm-2"></div>
            <div className="col-sm-8 padTop">
             <table className="table table-sm table-striped certificateTable">
                <thead>
                <tr>
                  <th scope="col">
                    Id
                  </th>
                  <th scope="col">
                    Certificate Name
                  </th>
                  <th scope="col">
                    Course
                  </th>
                  <th scope="col">
                    User Group
                  </th>
                  <th scope="col">
                    Created Date
                  </th>
                  <th scope="col">
                    Last Modified Date
                  </th>
                  <th scope="col">
                    Modified By
                  </th>
                </tr>
            </thead>
             <tbody>
            {this.state.ListItem.map((d) => <CertListItem key={d.Id} data={d} handleDelete={this.handleDelete} handleEdit={this.handleEdit}/>)}
            </tbody>
             </table>
            <br />
            <button className="btn btn-default" onClick={this.handleNewCert}>New Certificate</button>
           </div>
           <div className="col-sm-2"></div>
         </div>
        );
    }
};


/******** CertListItem ********/
class CertListItem extends React.Component {

constructor(props) {
    super(props);

        this.onDelete = this.onDelete.bind(this);
        this.onEdit = this.onEdit.bind(this);
   }

 onDelete(e) {
    e.preventDefault();
        
        var CertId = this.props.data.Id;
         this.props.handleDelete(CertId);
    }

onEdit(e) {
    e.preventDefault();

    var CertId = this.props.data.Id;
    this.props.handleEdit(CertId);
    }

 render () {
        const element = this.props.data;
        const createdDate = new Date(parseInt(element.CreationDate.substr(6)))
        const modifiedDate = new Date(parseInt(element.Modified.substr(6)))
        return (
        <tr>
           <th scope="row">
            {element.Id}
           </th>
           <td>
            {element.PageName}
           </td>
           <td>
            {(element.Coursekey || " - ")}
           </td>
           <td>
            {((element.UserGroupkey !== 1) || "-- Root --")}
           </td>
           <td>
            {moment(createdDate.toString()).format('MM/DD/YYYY')}
           </td>
           <td>
            {moment(modifiedDate.toString()).format('MM/DD/YYYY')}
           </td>
           <td>
            {element.ModifiedBy}
           </td>
           <td>
             <a href='#'  onClick={this.onEdit} className="editIcon"><i className="glyphicon glyphicon-pencil"></i></a>
           </td>
           <td>
            <a href='#'  onClick={this.onDelete} className="deleteIcon"><i className="glyphicon glyphicon-trash"></i></a>
           </td>
        </tr>
      );
    }
};

ReactDOM.render(<IndexView url="/getcertificatelist" />, document.getElementById("root"));

