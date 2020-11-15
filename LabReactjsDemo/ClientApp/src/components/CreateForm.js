import React, { Component } from 'react';
import axios  from 'axios';

export class CreateForm extends Component {
    static displayName = CreateForm.name;

    constructor(props) {
        super(props);
        this.state = {name: '', description: '', quantity: 0};

        this.handleInputChange = this.handleInputChange.bind(this);
    }

    async create(item) {
        console.log('create');
        await axios.post('/api/item', this.state);

        this.setState({name: '', description: '', quantity: 0})
        if(this.props) this.props.onCreated();
        
    }

    handleInputChange(event) {
        const target = event.target;
        const value = target.type === 'number' ? parseInt(target.value) : target.value;
        const name = target.name;
    
        this.setState({
          [name]: value
        });
      }

    render() {
        return (
            <div>
                <button type="button" className="btn btn-primary" onClick={() => this.create(this.state)}>Create</button>
                <div className="form-group">
                    <label htmlFor="name">Name</label>
                    <input type="text" className="form-control" id="name" placeholder="Name"
                    value={this.state.name}
                    name="name"
                    onChange={this.handleInputChange}/>
                </div>
                <div className="form-group">
                    <label htmlFor="description">Description</label>
                    <input type="text" className="form-control" id="description" placeholder="Description" 
                    value={this.state.description}
                    name="description"
                    onChange={this.handleInputChange}/>
                </div>
                <div className="form-group">
                    <label htmlFor="quantity">Quantity</label>
                    <input type="number" className="form-control" id="quantity" placeholder="Quantity" 
                    value={this.state.quantity}
                    name="quantity"
                    onChange={this.handleInputChange}/>
                </div>
            </div>
        );
    }
}
