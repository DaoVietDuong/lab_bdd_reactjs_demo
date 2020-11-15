import React, { Component } from 'react';
import { CreateForm } from './CreateForm';

export class FetchData extends Component {
  static displayName = FetchData.name;

  constructor(props) {
    super(props);
    this.state = { items: [], loading: true };
  }

  componentDidMount() {
    this.populateItem();
  }

  refresh() {
    this.populateItem();
  }

  static renderItemTable(items) {
      return (
      
      <table className='table table-striped' aria-labelledby="tabelLabel">
        <thead>
          <tr>
            <th>Id</th>
            <th>Name</th>
            <th>Description</th>
            <th>Quantity</th>
          </tr>
        </thead>
        <tbody>
          {items.map(item =>
            <tr key={item.id}>
              <td>{item.id}</td>
              <td>{item.name}</td>
              <td>{item.description}</td>
              <td>{item.quantity}</td>
            </tr>
          )}
        </tbody>
      </table>
    );
  }

  render() {
    let contents = this.state.loading
      ? <p><em>Loading...</em></p>
      : FetchData.renderItemTable(this.state.items);

    return (
      <div>
        <h1 id="tabelLabel" >Weather forecast</h1>
        <p>This component demonstrates fetching data from the server.</p>
            <CreateForm onCreated={this.refresh.bind(this)}/>

            {contents}  
      </div>
    );
  }

  async populateItem() {
    const response = await fetch('/api/item');
    const data = await response.json();
    this.setState({ items: data, loading: false });
  }
}
